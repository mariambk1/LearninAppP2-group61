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

        grid.ExecuteProgram(program);

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

        grid.ExecuteProgram(program);

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

        grid.ExecuteProgram(program);

        Assert.Equal((4, 0), (grid.GetPlayer().position.X, grid.GetPlayer().position.Y));
    }
}