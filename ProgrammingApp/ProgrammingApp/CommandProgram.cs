using System.Diagnostics;

namespace ProgrammingApp;

public interface ICommandProgram
{
    string Name { get; }
    List<ICommand> Commands {get;}
    void RunProgram(Grid grid);
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
    public void RunProgram(Grid grid)
    {
        Player player = grid.GetPlayer();
        List<string> trace = grid.GetTrace();

        foreach (ICommand command in Commands)
        {
            command.Execute(player, trace);
        }
    }
}