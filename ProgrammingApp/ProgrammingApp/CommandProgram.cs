namespace ProgrammingApp;

public interface ICommandProgram
{
    string Name { get; }
    List<ICommand> Commands {get;}
    void RunProgram(Player player, List<string> trace);
}

public class CommandProgram : ICommandProgram
{
    public string Name { get; }
    public List<ICommand> Commands {get;}

    public CommandProgram(string name, List<ICommand> commands)
    {
        Name = name;
        Commands = commands.ToList();
    }
    public void RunProgram(Player player, List<string> trace)
    {
        foreach (ICommand command in Commands)
        {
            command.Execute(player, trace);
        }
    }

}

public static class CommandLineInteraction
{
    public static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nChoose: 1. Basic  2. Advanced  3. Expert\n4. Import file 5. Exit\n");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine() ?? "";

            if (choice == "5") return;

            ICommandProgram program;

            try
            {
                program = choice switch
                {
                    "1" => ProgramExamples.GetRandom(ObjectType.Basic),
                    "2" => ProgramExamples.GetRandom(ObjectType.Advanced),
                    "3" => ProgramExamples.GetRandom(ObjectType.Expert),
                    "4" => ImportFile() , 
                    _ => throw new ArgumentException("Invalid choice.")
                };
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                continue;
            }

            Console.WriteLine($"\nSelected program: {program.Name}");            
            Console.WriteLine("1. Execute the program\n2. Calculate the metrics \n3. Go back\n");
            Console.Write("Choose action: \n");

            string action = Console.ReadLine() ?? "";

            if (action == "1")
            {
                var grid = new Grid();
                grid.ExecuteProgram(program);

                foreach (string step in grid.GetTrace())
                    Console.WriteLine(step);

                Console.WriteLine(grid.GetFinalState());
            }
            else if (action == "2")
            {
                var metrics = new Metrics(program.Commands);
                Console.WriteLine($"The command count is: {metrics.CommandCount}");
                Console.WriteLine($"The maximum nesting is: {metrics.MaxNestingLevel}");
                Console.WriteLine($"The repeat count is: {metrics.RepeatCount}");
            }
        }
    }

    private static CommandProgram ImportFile()
    {
        Console.Write("Enter path of the file that you want to import: ");
        string filePath = Console.ReadLine() ?? "";

        IProgramImport importer = new FileImport();

        return importer.ImportFromTextFile(filePath);
    }
}
