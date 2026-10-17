namespace ProgrammingApp;

public class Grid
{
    private Player player;
    private List<string> trace;

    public Grid()
    {
        player = new Player();
        trace = new List<string>();
    }

    public void ExecuteProgram(CommandProgram program)
    {
        player = new Player();
        trace = new List<string>();

        foreach (ICommand command in program.Commands)
        {
            command.Execute(player, trace);
        }
    }

    public List<string> GetTrace()
    {
        return trace;
    }

    public Player GetPlayer()
    {
        return player;
    }

    public string GetFinalState()
    {
        return $"End state ({player.position.X},{player.position.Y}) " +
               $"facing {player.direction.ToString()}.";
    }
}
