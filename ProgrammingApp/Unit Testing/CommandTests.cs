namespace Unit_Testing;
using ProgrammingApp;

public class CommandTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(3)]
    public void ValidMoveCommand(int steps)
    {
        Player player = new();
        List<string> trace = new();
        MoveCommand command = new(steps);

        command.Execute(player, trace);
        Assert.Equal((steps, 0), (player.position.X, player.position.Y));
        Assert.Equal(new[] { $"Move {steps}" }, trace);
    }

    [Theory]
    [InlineData(TurnDirection.Left, Direction.North, "Turn left")]
    [InlineData(TurnDirection.Right, Direction.South, "Turn right")]
    public void ValidTurnCommand(
        TurnDirection turnDirection,
        Direction expectedDirection,
        string expectedTrace)
    {
        Player player = new();
        List<string> trace = new();
        TurnCommand command = new(turnDirection);

        command.Execute(player, trace);
        Assert.Equal(expectedDirection, player.direction);
        Assert.Equal(new[] { expectedTrace }, trace);
    }

    [Fact]
    public void ValidRepeatCommand()
    {
        Player player = new();
        List<string> trace = new();
        List<ICommand> commands = new()
        {
            new MoveCommand(2),
            new TurnCommand(TurnDirection.Right)
        };
        RepeatCommand command = new(2, commands);

        command.Execute(player, trace);
        Assert.Equal((2, 2), (player.position.X, player.position.Y));
        Assert.Equal(Direction.West, player.direction);
        Assert.Equal(
            new[] { "Move 2", "Turn right", "Move 2", "Turn right" },
            trace);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(3)]
    public void ValidRepeatCount(int times)
    {
        Player player = new();
        List<string> trace = new();
        List<ICommand> commands = new()
        {
            new MoveCommand(2)
        };
        RepeatCommand command = new(times, commands);

        command.Execute(player, trace);
        Assert.Equal((times * 2, 0),
            (player.position.X, player.position.Y));
        Assert.Equal(Direction.East, player.direction);
        Assert.Equal(times, trace.Count);
        Assert.All(trace, step => Assert.Equal("Move 2", step));
    }
}
}    
