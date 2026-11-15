namespace ProgrammingApp;

//interface for execution commands
public interface ICommand
{
    void Execute(Player player, List<string> trace);
    string ToString();
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
        for (int i = 0; i < Steps; i++)
        {
            player.Move(1);
        }
       
        trace.Add($"Move {Steps}");
    }

    public String ToString()
    {
        return ($"Move {Steps}");
    }
}

public class TurnCommand : ICommand
{
    public TurnDirection Direction;

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

    public String ToString()
    {
        return ($"Turn {Direction}");
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

    public string ToString()
    {
        return ($"Repeat {Times}");
    }
}
