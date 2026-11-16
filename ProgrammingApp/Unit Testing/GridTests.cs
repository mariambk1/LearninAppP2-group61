using ProgrammingApp;

namespace Unit_Testing;

public class GridTests
{
    [Fact]
    public void ValidExecutionBasicProgram()
    {
        Grid grid = new();

        var program = new CommandProgram("Path", new List<ICommand>
        {
            new MoveCommand(5),
            new TurnCommand(TurnDirection.Left),
            new MoveCommand(3),
            new TurnCommand(TurnDirection.Right),
            new MoveCommand(2)
        });

        program.RunProgram(grid);

        Assert.Equal((7, -3), (grid.GetPlayer().position.X, grid.GetPlayer().position.Y));
    }

    [Fact]
    public void ValidExecutionAdvancedProgram()
    {
        Grid grid = new();

        var program = new CommandProgram("Square", new List<ICommand>
        {
            new RepeatCommand(4, new List<ICommand>
            {
                new MoveCommand(5),
                new TurnCommand(TurnDirection.Right)
            })
        });

        program.RunProgram(grid);

        Assert.Equal((0, 0), (grid.GetPlayer().position.X, grid.GetPlayer().position.Y));
    }

    [Fact]
    public void ValidExecutionExpertProgram()
    {
        Grid grid = new();

        var program = new CommandProgram("NestedSquare", new List<ICommand>
        {
            new RepeatCommand(2, new List<ICommand>
            {
                new RepeatCommand(4, new List<ICommand>
                {
                    new MoveCommand(3),
                    new TurnCommand(TurnDirection.Right)
                }),
                new MoveCommand(2)
            })
        });

        program.RunProgram(grid);

        Assert.Equal((4, 0), (grid.GetPlayer().position.X, grid.GetPlayer().position.Y));
    }

    [Fact]
    public void ValidIsWall()
    {
        string[] mapLines = {
                "o+o",
                "ooo",
                "ooo"
            };
        var temporaryPath = Path.GetTempFileName();
        File.WriteAllLines(temporaryPath, mapLines);

        Grid grid = new Grid();
        grid.LoadExercise(temporaryPath);

        Assert.True(grid.IsWall(1, 0));
        Assert.False(grid.IsWall(0, 0));
    }

    [Fact]
    public void ValidWallAheadError()
    {
        string[] mapLines = {
                "o+o",
                "ooo",
                "ooo"
            };
        var temporaryPath = Path.GetTempFileName();
        File.WriteAllLines(temporaryPath, mapLines);

        Grid grid = new Grid();
        grid.LoadExercise(temporaryPath);
        Player player = grid.GetPlayer();

        Assert.Throws<WallAheadError>(() =>
        {
            player.Move(1);
        });
    }

    [Fact]
    public void ValidGridEdgeError()
    {
        string[] mapLines = {
                "ooo",
                "ooo",
                "ooo"
            };
        var temporaryPath = Path.GetTempFileName();
        File.WriteAllLines(temporaryPath, mapLines);

        Grid grid = new Grid();
        grid.LoadExercise(temporaryPath);
        Player player = grid.GetPlayer(); 

        player.TurnLeft();

        Assert.Throws<GridEdgeError>(() =>
        {
            player.Move(1);
        });
    }

    [Fact]
    public void ValidTargetReached()
    {
        string[] mapLines = {
                "oox",
                "ooo",
                "ooo"
            };
        var temporaryPath = Path.GetTempFileName();
        File.WriteAllLines(temporaryPath, mapLines);

        Grid grid = new Grid();
        grid.LoadExercise(temporaryPath);
        Player player = grid.GetPlayer();

        Assert.False(grid.IsSuccessful());

        player.Move(2);

        Assert.True(grid.IsSuccessful());
    }
}