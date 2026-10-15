namespace ProgrammingApp;

public class Player
{
    public Position position;
    public Direction direction;

    // constructor voor nieuwe player
    public Player()
    {
        position = new Position(0, 0);
        direction = Direction.East;
    }

    //logic voor updaten van position en direction 
    public void Move(int amountSteps)
    {
        // movement per direction
        if (direction == Direction.North)
            position.Y -= amountSteps;
        else if (direction == Direction.East)
            position.X += amountSteps;
        else if (direction == Direction.South)
            position.Y += amountSteps;
        else if (direction == Direction.West)
            position.X -= amountSteps;
    }
    public void TurnLeft()
    {
        if (direction == Direction.North)
            direction = Direction.West;
        else if (direction == Direction.West)
            direction = Direction.South;
        else if (direction == Direction.South)
            direction = Direction.East;
        else if (direction == Direction.East)
            direction = Direction.North;
    }
    public void TurnRight()
    {
        if (direction == Direction.North)
            direction = Direction.East;
        else if (direction == Direction.East)
            direction = Direction.South;
        else if (direction == Direction.South)
            direction = Direction.West;
        else if (direction == Direction.West)
            direction = Direction.North;
    }

}
