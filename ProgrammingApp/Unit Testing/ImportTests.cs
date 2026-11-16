using ProgrammingApp;
namespace Unit_Testing;

public class FileImportTests
{
    private static List<ICommand> Parse(string text)
    {
        string[] lines = text.Split('\n');
        int lineIndex = 0;

        return new FileImport().ReadCommands(lines, ref lineIndex, 0);
    }

    [Fact]
    public void ParsesMoveSteps()
    {
        var commands = Parse("Move 3");
        var move = Assert.IsType<MoveCommand>(Assert.Single(commands));

        Assert.Equal(3, move.Steps);
    }

    [Theory]
    [InlineData("Turn Left", TurnDirection.Left)]
    [InlineData("Turn Right", TurnDirection.Right)]
    public void ParsesTurnDirection(
        string text,
        TurnDirection expectedDirection)
    {
        var commands = Parse(text);
        var turn = Assert.IsType<TurnCommand>(Assert.Single(commands));

        Assert.Equal(expectedDirection, turn.Direction);
    }

    [Fact]
    public void ParsesRepeatCount()
    {
        var commands = Parse("Repeat 3 times\n" + "    Move 1");
        var repeat =Assert.IsType<RepeatCommand>(Assert.Single(commands));

        Assert.Equal(3, repeat.Times);
    }

    [Fact]
    public void ParsesRepeatBody()
    {
        var commands = Parse("Repeat 3 times\n" + "    Move 2");
        var repeat = Assert.IsType<RepeatCommand>(Assert.Single(commands));
        var move = Assert.IsType<MoveCommand>(Assert.Single(repeat.Commands));

        Assert.Equal(2, move.Steps);
    }

    [Theory]
    [InlineData("WallAhead", RepeatUntilCondition.WallAhead)]
    [InlineData("GridEdge", RepeatUntilCondition.GridEdge)]
    public void ParsesRepeatUntilCondition(string conditionText, RepeatUntilCondition expectedCondition)
    {
        var commands = Parse($"RepeatUntil {conditionText}\n" + "    Move 1");
        var until =Assert.IsType<RepeatUntilCommand>(Assert.Single(commands));

        Assert.Equal(expectedCondition, until.Condition);
    }

    [Fact]
    public void ParsesNestedRepeatUntilBody()
    {
        var commands = Parse("Repeat 2 times\n" + "    RepeatUntil WallAhead\n" + "        Move 4");
        var repeat = Assert.IsType<RepeatCommand>(Assert.Single(commands));
        var until = Assert.IsType<RepeatUntilCommand>(Assert.Single(repeat.Commands));
        var move =Assert.IsType<MoveCommand>(Assert.Single(until.Commands));

        Assert.Equal(4, move.Steps);
    }

    [Theory]
    [InlineData("Jump 1")]
    [InlineData("RepeatUntil UnknownCondition\n    Move 1")]
    [InlineData("Repeat 2 times\n  Move 1")]
    [InlineData("Repeat 2 times")]
    public void InvalidProgramThrowsFormatException(string text)
    {
        Assert.Throws<FormatException>(() => Parse(text));
    }
}