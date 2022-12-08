namespace day7;

public class Program
{
    private static async Task Main(string[] args)
    {
        var input = await File.ReadAllLinesAsync("./input.txt");
        var first = SolveFirstProblem(input);
        var second = SolveSecondProblem(input);
        // if (second >= 4094)
        //     throw new Exception("value too high");

        Console.WriteLine($"First: {first}");
        Console.WriteLine($"Second: {second}");
    }

    public static long SolveFirstProblem(string[] input) {
        TestDirectory root = null;
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
                        continue;
                    }

                    if (currentDirectory == null) {
                        root = new TestDirectory(){
                            Name = directory
                        };

                        currentDirectory = root;
                        continue;
                    }


                    currentDirectory = currentDirectory.SubDirectories.Find(d => d.Name == directory);

                    break;
                case 'd':
                    if (currentDirectory == null)
                        throw new Exception();

                    currentDirectory.SubDirectories.Add(new TestDirectory(){
                        Name = trimmed.Split(" ").Last().Trim(),
                        ParentDirectory = currentDirectory
                    });
                    // handle directory
                    break;
                default:
                    var parts = trimmed.Split(" ");
                    if (parts.Length != 2 || currentDirectory == null)
                        throw new Exception();

                    var size = parts[0];
                    var fileName = parts[1];
                    currentDirectory.Files.Add(new TestFile(){
                        Name = fileName,
                        Size = long.Parse(size)
                    });
                    // should be file size
                    break;
            }
        }

        var subDirs = GetDirs(root);

        var sum = subDirs.Sum(s => s.Size);

        return sum;
    }

    public static long SolveSecondProblem(string[] input) {
        TestDirectory root = null;
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
                        continue;
                    }

                    if (currentDirectory == null) {
                        root = new TestDirectory(){
                            Name = directory
                        };

                        currentDirectory = root;
                        continue;
                    }


                    currentDirectory = currentDirectory.SubDirectories.Find(d => d.Name == directory);

                    break;
                case 'd':
                    if (currentDirectory == null)
                        throw new Exception();

                    currentDirectory.SubDirectories.Add(new TestDirectory(){
                        Name = trimmed.Split(" ").Last().Trim(),
                        ParentDirectory = currentDirectory
                    });
                    // handle directory
                    break;
                default:
                    var parts = trimmed.Split(" ");
                    if (parts.Length != 2 || currentDirectory == null)
                        throw new Exception();

                    var size = parts[0];
                    var fileName = parts[1];
                    currentDirectory.Files.Add(new TestFile(){
                        Name = fileName,
                        Size = long.Parse(size)
                    });
                    // should be file size
                    break;
            }
        }

        var necessarySize = 30000000 - (70000000 - root.Size);
        var subDirs = GetDirs1(root, necessarySize);

        var toDelete = subDirs.Min(s => s.Size);

        return toDelete;
    }

    static List<TestDirectory> GetDirs(TestDirectory directory) {

        var list = new List<TestDirectory>();
        if (directory.Size <= 100000)
        {
            list.Add(directory);
            // return list;
        }

        foreach (var dir in directory.SubDirectories) {
            var dirs = GetDirs(dir);
            list.AddRange(dirs);
        }

        return list;
    }

    static List<TestDirectory> GetDirs1(TestDirectory directory, long size) {

        var list = new List<TestDirectory>();
        if (directory.Size >= size)
        {
            list.Add(directory);
            // return list;
        }

        foreach (var dir in directory.SubDirectories) {
            var dirs = GetDirs1(dir, size);
            list.AddRange(dirs);
        }

        return list;
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

    public long Size => SubDirectories.Sum(d => d.Size) + Files.Sum(f => f.Size);
}
