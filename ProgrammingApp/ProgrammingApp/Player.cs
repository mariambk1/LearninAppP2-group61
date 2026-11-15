namespace ProgrammingApp;

public class Player
{
    public Position position;
    public Direction direction;
    public Grid grid;

    public Player()
    {
        position = new Position(0, 0);
        direction = Direction.East;
    }

    public void Move(int amountSteps)
    {
        for (int i = 0; i < amountSteps; i++)
        {
            int x = position.X;
            int y = position.Y;

            if (direction == Direction.North)
                y --;
            else if (direction == Direction.East)
                x ++;
            else if (direction == Direction.South)
                y ++;
            else if (direction == Direction.West)
                x --;

            if (grid != null && grid.HasExercise())
            {
                if (grid.IsOutsideGrid(x, y))
                    throw new GridEdgeError();

                if (grid.IsWall(x, y))
                    throw new WallAheadError();
            }

            position.X = x;
            position.Y = y;

            if (grid != null)
                grid.AddToPath();
        }
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
