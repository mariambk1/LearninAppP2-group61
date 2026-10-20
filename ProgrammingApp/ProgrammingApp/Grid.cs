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

    public void ExecuteProgram(ICommandProgram program)
    {
        player = new Player();
        trace = new List<string>();

        program.RunProgram(player, trace);
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
        return $"\nEnd position is ({player.position.X},{player.position.Y}) " +
               $"while facing {player.direction.ToString()}.";
    }
}
