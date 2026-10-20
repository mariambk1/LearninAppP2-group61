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

    private void CalculateCommands(IEnumerable<ICommand> commands, int nestingLevel)
    {
        foreach (ICommand command in commands)
        {   
            // elke command telt 1 op
            CommandCount++;

            // als het een repeat is komt er ook 1 bij repeatcount
            if (command is RepeatCommand repeatCommand)
            {   
                RepeatCount++;

                // nestinglevel +1 als er commands binnen dez repeat zitten
                int childLevel = nestingLevel + 1;

                // hoogste waarde wordt opgeslagen
                MaxNestingLevel = Math.Max(MaxNestingLevel, childLevel);

                // recursion, als er nog een repeat in de repeat zit, gaat de childlevel omhoog
                CalculateCommands(repeatCommand.Commands, childLevel);
            }
        }
    }
}