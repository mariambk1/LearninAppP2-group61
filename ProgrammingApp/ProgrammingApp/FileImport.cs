namespace ProgrammingApp;

public interface IProgramImport
{
    CommandProgram ImportFromTextFile(string filePath);
}

public class FileImport : IProgramImport
{
    public CommandProgram ImportFromTextFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Could not find file at {filePath}");
        }

        string[] lines = File.ReadAllLines(filePath);

        if (lines.Length == 0)
        {
            throw new ArgumentException("Empty file");
        }

        int lineIndex = 0;

        List<ICommand> commands = ReadCommands(lines, ref lineIndex, 0);
        string name = Path.GetFileNameWithoutExtension(filePath);

        return new CommandProgram(name, commands);
    }

    private List<ICommand> ReadCommands(string[] lines, ref int lineIndex, int currentNesting)
    {
        List<ICommand> commands = new List<ICommand>();

        while (lineIndex < lines.Length)
        {
            int lineNumber = lineIndex + 1;
            string line = lines[lineIndex];

            if (string.IsNullOrWhiteSpace(line))
            {
                lineIndex++;
                continue;
            }

            int spaces = 0;
            int indentation = 4; 

            int nesting = 0;

            foreach (char character in line)
            {
                if (character == ' ')
                {
                    spaces++;
                }
                else
                {
                    break;
                }
            }

            if (spaces % indentation != 0)
            {
                throw new FormatException($"Indentation on line {lineNumber} is not correct.");
            }

            nesting = spaces / indentation;

            if (nesting < currentNesting)
            {
                break;
            }

            if (nesting != currentNesting)
            {
                throw new FormatException($"Indentation on line {lineNumber} is not correct.");
            }

            string command = line.Trim();
            lineIndex++;

            if (command == "Turn left")
            {
                commands.Add(new TurnCommand(TurnDirection.Left));
            }
            else if (command == "Turn right")
            {
                commands.Add(new TurnCommand(TurnDirection.Right));
            }
            else if (command.StartsWith("Move "))
            {
                if (!int.TryParse(command.Substring(5), out int steps))
                {
                    throw new FormatException($"Invalid Move command on line {lineNumber}.");
                }

                commands.Add(new MoveCommand(steps));
            }
            else if (command.StartsWith("Repeat ") &&
                     command.EndsWith(" times"))
            {
                string[] parts = command.Split(' ');

                if (parts.Length != 3 ||
                    !int.TryParse(parts[1], out int times))
                {
                    throw new FormatException($"Invalid Repeat command on line {lineNumber}.");
                }

                if (times < 0)
                {
                    throw new FormatException($"Repeat cannot be negative on line {lineNumber}.");
                }

                List<ICommand> repeatCommands = ReadCommands(lines, ref lineIndex, nesting + 1);
                if (repeatCommands.Count == 0)
                {
                    throw new FormatException($"Repeat has no commands on line {lineNumber}.");
                }

                commands.Add(new RepeatCommand(times, repeatCommands));
            }
            else if (command.StartsWith("RepeatUntil "))
            {
                string conditionName= command.Substring(12);

                if (!Enum.TryParse(conditionName, out RepeatUntilCondition condition))
                {
                    throw new FormatException($"Invalid RepeatUntil condition on line {lineNumber}.");
                }

                List<ICommand> repeatCommands = ReadCommands(lines, ref lineIndex, nesting + 1);

                if (repeatCommands.Count == 0)
                {
                    throw new FormatException($"RepeatUntil has no commands on line {lineNumber}.");
                }

                commands.Add(new RepeatUntilCommand(condition, repeatCommands));
            }
            else
            {
                throw new FormatException($"unknown command on line {lineNumber}: {command}");
            }
        }

        return commands;
    }
}