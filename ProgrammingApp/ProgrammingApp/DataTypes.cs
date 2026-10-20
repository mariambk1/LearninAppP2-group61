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

    // geeft een string van de positie van de player
    public string ToString()
    {
        string posString = $"End state ({X}, {Y})";
        return posString;
    }
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

public enum ObjectType
{
    Basic,
    Advanced,
    Expert
}