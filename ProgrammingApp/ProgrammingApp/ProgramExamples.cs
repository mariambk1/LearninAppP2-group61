
namespace ProgrammingApp;

public static class ProgramExamples
{
    private static Random random = new();

    // meerdere basic objects toevoegen, 3 genoeg denk ik.  
    private static List<CommandProgram> basicPrograms =
    [
        new CommandProgram("Path", new List<ICommand>
        {
            new MoveCommand(5),
            new TurnCommand(TurnDirection.Left),
            new MoveCommand(3),
            new TurnCommand(TurnDirection.Right),
            new MoveCommand(2)
        })
    ];

    // zelfde voor advanced
    private static List<CommandProgram> advancedPrograms =
    [
        new CommandProgram("Square", new List<ICommand>
        {
            new RepeatCommand(4, new List<ICommand>
            {
                new MoveCommand(5),
                new TurnCommand(TurnDirection.Right)
            })
        })
    ];

    // en ook zo voor expert
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
        })
    ];


    // random programma kiezen wanneer we meerdere objects hebben per level, heb er nu nog 1 object per level staan 
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
