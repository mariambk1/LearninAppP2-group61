namespace ProgrammingApp;

public interface ICommandProgram
{
    string Name { get; }
    List<ICommand> Commands {get;}
    void RunProgram(Player player, List<string> trace);
}

public class CommandProgram : ICommandProgram
{
    public string Name { get; }
    public List<ICommand> Commands {get;}

    public CommandProgram(string name, List<ICommand> commands)
    {
        Name = name;
        Commands = commands.ToList();
    }
    public void RunProgram(Player player, List<string> trace)
    {
        foreach (ICommand command in Commands)
        {
            command.Execute(player, trace);
        }
    }

}