namespace ProgrammingApp;

public interface IProgramImport
{
    CommandProgram ImportFromTextFile(string filePath);
}

public class FileImport : IProgramImport
{
    public CommandProgram ImportFromTextFile(string filePath)
    {
        if (!File.Exists(filePath)
            throw new FileNotFoundException($"Could not find file at {filePath}");

        string[] lines = File.ReadAllLines(filePath);
        if (lines.Length == 0)
            throw new ArgumentException("Empty file");

    }
}
