using System.Runtime.CompilerServices;

namespace ProgrammingApp;

public class Grid
{
    private Player player;
    private List<string> trace;
    private List<Position> path = new();
    private bool exerciseLoaded = false;

    private char[,] cells;
    public int cols;
    public int cellWidth;
    public int cellHeight;
    public int rows;

    private int animationIndex = 0;
    public bool IsAnimating => animationIndex < path.Count;

    public Grid()
    {
        player = new Player();
        player.grid = this;
        trace = new List<string>();
        cols = 5;
        rows = 5;
        cells = new char[5, 5];
        cellWidth = 120;
        cellHeight = 120;

        path.Add(new Position(0, 0));
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

    public void Reset()
    {
        player = new();
        player.grid = this;
        trace.Clear();
        path.Clear();
        path.Add(new Position(0, 0));
    }

    public void LoadExercise(string filename)
    {
        string[] lines = File.ReadAllLines(filename);

        rows = lines.Length;
        cols = lines[0].Length;
        cells = new char[rows, cols];

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                cells[y, x] = lines[y][x];
            }
        }

        exerciseLoaded = true;
        player = new Player();
        player.grid = this;
        trace.Clear();
        path.Clear();
        path.Add(new Position(0, 0));
    }

    public char GetCell(int x, int y)
    {
        return cells[y, x];
    }

    public bool IsOutsideGrid(int x, int y)
    {
        return x < 0 || y < 0 || x >= cols || y >= rows;
    }

    public bool IsWall(int x, int y)
    {
        return exerciseLoaded && !IsOutsideGrid(x, y)
            && cells[y, x] == '+';
    }

    public bool HasExercise()
    {
        return exerciseLoaded;
    }

    public bool IsSuccessful()
    {
        return exerciseLoaded &&
            !IsOutsideGrid(player.position.X, player.position.Y) &&
            GetCell(player.position.X, player.position.Y) == 'x';
    }

    public bool CheckCondition(RepeatUntilCondition condition)
    {
        if (!exerciseLoaded)
            return false;

        int x = player.position.X;
        int y = player.position.Y;

        if (player.direction == Direction.North) y--;
        if (player.direction == Direction.East) x++;
        if (player.direction == Direction.South) y++;
        if (player.direction == Direction.West) x--;

        if (condition == RepeatUntilCondition.WallAhead)
            return IsWall(x, y);

        if (condition == RepeatUntilCondition.GridEdge)
        {
            return IsOutsideGrid(x, y);
        }

        return false;
    }

    public List<Position> GetPath()
    {
        return path;
    }

    public void AddToPath()
    {
        path.Add(new Position(player.position.X, player.position.Y));
    }

    public void StartAnimation()
    {
        animationIndex = 0;
        if (path.Count > 0)
        {
            player.position.X = path[0].X;
            player.position.Y = path[0].Y;
        }
    }

    public void StepAnimation()
    {
        if (animationIndex < path.Count)
        {
            player.position.X = path[animationIndex].X;
            player.position.Y = path[animationIndex].Y;
            animationIndex++;
        }
    }
}

public class WallAheadError : Exception
{
    public WallAheadError() : base("can't move into a wall.")
    {
    }
}

public class GridEdgeError : Exception
{
    public GridEdgeError() : base("can't move outside the grid.")
    {
    }
}