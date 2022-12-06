namespace day6;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var input = await File.ReadAllTextAsync("./input.txt");
        var first = solveFirstProblem(input);
        var second = solveSecondProblem(input);
        if (second >= 4094)
            throw new Exception("value too high");

        Console.WriteLine($"First: {first}");
        Console.WriteLine($"Second: {second}");
    }

    public static int solveFirstProblem(string input) {
        var result = 0;
        var buffer = new List<char>();
        foreach(var (c, index) in input.WithIndex()) {
            if (buffer.Count > 4)
                break;

            if (!buffer.Contains(c)) {
                result = index;
                buffer.Add(c);
                continue;
            }

            foreach(var t in new List<char>(buffer)) {
                if (t != c) {
                    buffer.RemoveAt(0);
                    continue;
                }

                buffer.RemoveAt(0);
                break;
            }

            // buffer.RemoveAt(0);
            buffer.Add(c);
            result = index;
        }
        return result;
    }

    public static int solveSecondProblem(string input) {
        var result = 0;
        var buffer = new List<char>();
        foreach(var (c, index) in input.WithIndex()) {
            if (buffer.Count > 14) {
                break;
            }

            if (buffer.Count == 14) {
                result = index;
                break;
            }

            if (!buffer.Contains(c)) {
                result = index;
                buffer.Add(c);
                continue;
            }

            var t1 = 1;

            foreach(var t in new List<char>(buffer)) {
                if (t != c) {
                    buffer.RemoveAt(0);
                    continue;
                }

                buffer.RemoveAt(0);
                break;
            }
            buffer.Add(c);
            result = index;
        }
        return result;
    }
}
