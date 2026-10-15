namespace ProgrammingApp;

//interface for execution commands
public interface ICommand
{
    void Execute(Player player);
}

//classes to implement and execute commands
public class MoveCommand : ICommand
{
    int steps;
}

public class TurnCommand : ICommand
{
    TurnDirection direction;
}

public class RepeatCommand : ICommand
{
    int times;
    //Commands list<commands>
}
