namespace ProgrammingApp;

public class Metrics
{
    public int CommandCount{get; set;}
    public int MaxNestingLevel{get; set;}
    public int RepeatCount{get; set;}

    public Metrics(IEnumerable<ICommand> commands)
    {
        CalculateCommands(commands, 0);
    }

    private void CalculateCommands(
        IEnumerable<ICommand> commands,
        int nestingLevel)
    {
        foreach (ICommand command in commands)
        {
            CommandCount++;
            if (command is RepeatCommand repeatCommand)
            {
                RepeatCount++;
                int childLevel = nestingLevel + 1;
                MaxNestingLevel =
                    Math.Max(MaxNestingLevel, childLevel);
                CalculateCommands(
                    repeatCommand.Commands,
                    childLevel);
            }
        }
    }
}