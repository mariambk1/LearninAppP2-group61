namespace ProgrammingApp;

public interface ICommandProgram
{
    void RunProgram(Player player);
}

public class CommandProgram : ICommandProgram
{
    public string name { get; }
    private Metrics metrics;
    private Metrics metrics;

    public CommandProgram(string name, List<ICommand> commands)
    {
        //Store name+ commands
    }
    public void RunProgram(Player player)
    {
        
    }
}
