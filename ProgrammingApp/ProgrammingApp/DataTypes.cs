namespace ProgrammingApp;

public class Position
{
    public int X { get; set; }
    public int Y { get; set; }

    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }

    // translate function, maar is dit wel nodig als we alle logic in Move() hebben?
}

public enum Direction
{
    North,
    East,
    South,
    West
}

public enum TurnDirection
{
    Left,
    Right
}
