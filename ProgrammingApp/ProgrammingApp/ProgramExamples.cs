namespace ProgrammingApp;

public static class ProgramExamples
{
    private static Random random = new();

    private static List<CommandProgram> basicPrograms =
    [
        new CommandProgram("Path", new List<ICommand>
        {
            new MoveCommand(5),
            new TurnCommand(TurnDirection.Left),
            new MoveCommand(3),
            new TurnCommand(TurnDirection.Right),
            new MoveCommand(2)
        }),

        new CommandProgram("Rectangle", new List<ICommand>
        {
            new MoveCommand(8),
            new TurnCommand(TurnDirection.Right),
            new MoveCommand(4),
            new TurnCommand(TurnDirection.Right),
            new MoveCommand(8),
            new TurnCommand(TurnDirection.Right),
            new MoveCommand(4),
            new TurnCommand(TurnDirection.Right)
        }),

        new CommandProgram("L-path", new List<ICommand>
        {
            new MoveCommand(6),
            new TurnCommand(TurnDirection.Left),
            new MoveCommand(3)
        })
    ];

    private static List<CommandProgram> advancedPrograms =
    [
        new CommandProgram("Square", new List<ICommand>
        {
            new RepeatCommand(4, new List<ICommand>
            {
                new MoveCommand(5),
                new TurnCommand(TurnDirection.Right)
            })
        }),

        new CommandProgram("Stairs up", new List<ICommand>
        {
            new RepeatCommand(4, new List<ICommand>
            {
                new MoveCommand(3),
                new TurnCommand(TurnDirection.Left),
                new MoveCommand(2),
                new TurnCommand(TurnDirection.Right)
            })
        }),

        new CommandProgram("Stairs down", new List<ICommand>
        {
            new RepeatCommand(4, new List<ICommand>
            {
                new MoveCommand(3),
                new TurnCommand(TurnDirection.Right),
                new MoveCommand(2),
                new TurnCommand(TurnDirection.Left)
            })
        })
    ];

    private static List<CommandProgram> expertPrograms =
    [
        new CommandProgram("NestedSquare", new List<ICommand>
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
        }),

        new CommandProgram("NestedRectangle", new List<ICommand>
        {
            new RepeatCommand(2, new List<ICommand>
            {
                new RepeatCommand(2, new List<ICommand>
                {
                    new MoveCommand(8),
                    new TurnCommand(TurnDirection.Right),
                    new MoveCommand(4),
                    new TurnCommand(TurnDirection.Right)
                }),
                new MoveCommand(3)
            })
        }),

        new CommandProgram("NestedStairs", new List<ICommand>
        {
            new RepeatCommand(2, new List<ICommand>
            {
                new RepeatCommand(3, new List<ICommand>
                {
                    new MoveCommand(2),
                    new TurnCommand(TurnDirection.Left),
                    new MoveCommand(2),
                    new TurnCommand(TurnDirection.Right)
                }),
                new TurnCommand(TurnDirection.Right),
                new MoveCommand(3),
                new TurnCommand(TurnDirection.Left)
            })
        })
    ];

    // choosing a random program from the selected difficulty level
    public static CommandProgram GetRandom(ObjectType level)
    {
        List<CommandProgram> programs = level switch
        {
            ObjectType.Basic => basicPrograms,
            ObjectType.Advanced => advancedPrograms,
            ObjectType.Expert => expertPrograms,
            _ => throw new Exception("Invalid level")
        };

        return programs[random.Next(programs.Count)];
    }
}
