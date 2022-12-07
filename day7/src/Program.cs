namespace day7;

public class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    public static long SolveProblem1(string[] input) {
        var fileSystem = new List<TestDirectory>();
        TestDirectory currentDirectory = null;
        foreach (var line in input) {
            var trimmed = line.Trim().TrimEnd('\r');
            switch(trimmed[0]) {
                case '$':
                    // handle command
                    var directory = HandleCommand(trimmed);
                    if (directory == null)
                    {
                        // next lines should be a list of files to add to the current directory
                        continue;
                    }

                    if (directory == "..")
                    {
                        if (currentDirectory == null)
                            throw new Exception("tried to go to a null directory");

                        currentDirectory = currentDirectory.ParentDirectory;
                    }

                    var newDir = new TestDirectory(){
                        Name = directory
                    };

                    currentDirectory = newDir;

                    fileSystem.Add(newDir);
                    break;
                case 'd':
                    // handle directory
                    break;
                default:
                    // should be file size
                    break;
            }
        }

        return 0;
    }

    private static string? HandleCommand(string line) {
        switch(line)
        {
            case string a when a.Contains("ls"):
                // next lines contain list of directories or files bafore the next command
                return null;
            case string b when b.Contains("cd"):
                // changing directory, next line should be a new command
                return line.Split("cd").Last().Trim();
            default:
                throw new Exception($"Cannot handle command: {line}");
        }
    }
}

internal class TestFile {

    public string Name { get; set; }
    public long Size { get; set; }
}

internal class TestDirectory { 
    public TestDirectory ParentDirectory { get; set; }
    public string Name { get; set; }
    public List<TestFile> Files { get; set; } = new List<TestFile>();
    public List<TestDirectory> SubDirectories { get ;set; } = new List<TestDirectory>();
}
