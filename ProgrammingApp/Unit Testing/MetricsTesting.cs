using ProgrammingApp;
using System.Data.Common;

namespace Unit_Testing;

public class MetricsTesting
{
    [Fact]
    public void ValidMetricsBasicProgram()
    {
        var commands = new List<ICommand>
        {
            new MoveCommand(5),
            new TurnCommand(TurnDirection.Left),
            new MoveCommand(3),
            new TurnCommand(TurnDirection.Right),
            new MoveCommand(2)
        };

        var metrics = new Metrics(commands);

        Assert.Equal(5, metrics.CommandCount);
        Assert.Equal(0, metrics.MaxNestingLevel);
        Assert.Equal(0, metrics.RepeatCount);
    }

    [Fact]
    public void ValidMetricsAdvancedProgram()
    {
        var commands = new List<ICommand>
        {
            new RepeatCommand(4, new List<ICommand>
            {
                new MoveCommand(5),
                new TurnCommand(TurnDirection.Right)
            })
        };

        var metrics = new Metrics(commands);

        Assert.Equal(3, metrics.CommandCount);
        Assert.Equal(1, metrics.MaxNestingLevel);
        Assert.Equal(1, metrics.RepeatCount);
    }

    [Fact]
    public void ValidMetricsExpertProgram()
    {
        var commands = new List<ICommand>
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
        };

        var metrics = new Metrics(commands);

        Assert.Equal(5, metrics.CommandCount);
        Assert.Equal(2, metrics.MaxNestingLevel);
        Assert.Equal(2, metrics.RepeatCount);
    }
}
