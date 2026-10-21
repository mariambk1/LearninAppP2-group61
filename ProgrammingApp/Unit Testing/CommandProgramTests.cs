using ProgrammingApp;

namespace Unit_Testing;

public class CommandProgramTests
{
    [Fact]
    public void ValidRunBasicProgram()
    {
        var player = new Player();
        var trace = new List<String>();
        var program = new CommandProgram("Path", new List<ICommand>
        {
            new MoveCommand(5),
            new TurnCommand(TurnDirection.Left),
            new MoveCommand(3),
            new TurnCommand(TurnDirection.Right),
            new MoveCommand(2)
        });

        program.RunProgram(player, trace);

        Assert.Equal((7,-3), (player.position.X, player.position.Y));
    }

    [Fact]
    public void ValidRunAdvancedProgram()
    {
        var player = new Player();
        var trace = new List<String>();
        var program = new CommandProgram("Square", new List<ICommand>
        {
            new RepeatCommand(4, new List<ICommand>
            {
                new MoveCommand(5),
                new TurnCommand(TurnDirection.Right)
            })
        });

        program.RunProgram(player, trace);

        Assert.Equal((0,0), (player.position.X, player.position.Y));
    }

    [Fact]
    public void ValidRunExpertProgram()
    {
        var player = new Player();
        var trace = new List<String>();
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

        program.RunProgram(player, trace);

        Assert.Equal((4,0), (player.position.X, player.position.Y));
    }
}
