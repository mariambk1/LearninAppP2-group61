namespace ProgrammingApp;

//interface for execution commands
public interface ICommand
{
    void Execute(Player player);
}


/* 
    classes to implement and execute commands
*/

public class MoveCommand : ICommand
{
    private int steps;
    public MoveCommand(int _steps)
    {
        steps = _steps;
    }

    public void Execute(Player player)
    {
        player.Move(steps);
    }
}

public class TurnCommand : ICommand
{
    TurnDirection direction;

    public TurnCommand(TurnDirection _direction)
    {
        direction = _direction;
    }
    public void Execute(Player player)
    {
        if (direction == TurnDirection.Left)
            player.TurnLeft();
        else
            player.TurnRight();
    }
}

public class RepeatCommand : ICommand
{
    int times;
    private List<ICommand> commands = new();

    public RepeatCommand(int _times)
    {
        times = _times;
    }

    public void Execute(Player player)
    {
        
    }
}
