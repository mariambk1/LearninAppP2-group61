namespace ProgrammingApp;

//interface for execution commands
public interface ICommand
{
    void Execute(Player player, List<string> trace);
}

//classes to implement and execute commands
public class MoveCommand : ICommand
{
    public int Steps{get;}

    public MoveCommand(int steps)
    {
        Steps = steps;
    }
    public void Execute(Player player, List<string> trace)
    {
        player.Move(Steps);
        trace.Add($"Move {Steps}");
    }
}

public class TurnCommand : ICommand
{
    private TurnDirection Direction;

    public TurnCommand(TurnDirection direction)
    {
        Direction = direction;
    }
    public void Execute(Player player, List<string> trace)
    {
        if (Direction == TurnDirection.Left)
        {
            player.TurnLeft();
            trace.Add("Turn left");
        }
        else
        {
            player.TurnRight();
            trace.Add("Turn right");
        }
    }
}

public class RepeatCommand : ICommand
{
    public int Times{get;}
    public List<ICommand> Commands{get;}

    public RepeatCommand(int times, List<ICommand> commands)
    {
        Times = times;
        Commands = commands.ToList();
    }
    public void Execute(Player player, List<string> trace)
    {
         for (int i = 0; i < Times; i++)
        {
            foreach (ICommand command in Commands)
            {
                command.Execute(player, trace);
            }
        }
    }
}
