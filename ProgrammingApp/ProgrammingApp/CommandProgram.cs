using System.Diagnostics;

namespace ProgrammingApp;

public class CommandProgram
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