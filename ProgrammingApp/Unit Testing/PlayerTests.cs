namespace Unit_Testing;
using ProgrammingApp;

public class PlayerTests
{
    [Theory]
    [InlineData(Direction.North, 5, 0, -5)]
    [InlineData(Direction.East, 10, 10, 0)]
    [InlineData(Direction.South, 8, 0, 8)]
    [InlineData(Direction.West, 2, -2, 0)]
    [InlineData(Direction.North, 0, 0, 0)]
    [InlineData(Direction.East, 1, 1, 0)]
    [InlineData(Direction.South, 12, 0, 12)]
    [InlineData(Direction.West, 21, -21, 0)]

    public void ValidMovement(Direction direction, int steps, int expectedPosX, int expectedPosY)
    {
        Player player = new();
        player.direction = direction;
        player.Move(steps);
        var resultX = player.position.X;
        var resultY = player.position.Y;

        Assert.Equal((expectedPosX, expectedPosY), (resultX, resultY));
    }

    [Theory]
    [InlineData(Direction.North, Direction.West)]
    [InlineData(Direction.West, Direction.South)]
    [InlineData(Direction.South, Direction.East)]
    [InlineData(Direction.East, Direction.North)]
    public void ValidTurnLeft(Direction direction, Direction expectedDirection)
    {
        Player player = new();
        player.direction = direction;
        player.TurnLeft();

        Assert.Equal(expectedDirection, player.direction);
    }

    [Theory]
    [InlineData(Direction.North, Direction.East)]
    [InlineData(Direction.East, Direction.South)]
    [InlineData(Direction.South, Direction.West)]
    [InlineData(Direction.West, Direction.North)]
    public void ValidTurnRight(Direction direction, Direction expectedDirection)
    {
        Player player = new();
        player.direction = direction;
        player.TurnRight();

        Assert.Equal(expectedDirection, player.direction);
    }
}
