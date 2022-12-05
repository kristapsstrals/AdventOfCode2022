using System.Text.RegularExpressions;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var testData = @"    [D]    
[N] [C]    
[Z] [M] [P]
 1   2   3 

move 1 from 2 to 1
move 3 from 1 to 3
move 2 from 2 to 1
move 1 from 1 to 2";

        var testInput = testData.Split(Environment.NewLine);
        var input = await File.ReadAllLinesAsync("./input.txt");

        var firstTest = solveFirstProblem(testInput);
        if (firstTest != "CMZ")
            throw new Exception($"Invalid result for first case: {firstTest}");
        var first = solveFirstProblem(input);

        var secondTest = solveSecondProblem(testInput);
        if (secondTest != "MCD")
            throw new Exception($"Invalid result for second case: {secondTest}");
        var second = solveSecondProblem(input);
        Console.WriteLine($"First: {first}");
        Console.WriteLine($"Second: {second}");
    }

    static string solveFirstProblem(string[] input) {
        var result = "";

        var res = new Dictionary<int, List<string>>();
        var moves = new Queue<Move>();

        foreach(var line in input) {
            if (line.StartsWith("move")) {
                // movement
                var numbers = line.TrimEnd('\r').Split(" ").Where(s => Int32.TryParse(s, out _)).Select(s => Int32.Parse(s)).ToList();
                moves.Enqueue(new Move {
                    Count = numbers[0],
                    From = numbers[1],
                    To = numbers[2]
                });
                continue;
            }

            if (!line.Contains("[")) {
                // This is the number line
                continue;
            }

            if (line.Equals(String.Empty)) {
                // The line before move line
                continue;
            }

            // crates
            var data = ProcessLine(line);
            foreach(var (d, index) in data.WithIndex()) {
                if (!res.ContainsKey(index + 1)) {
                    res[index+1] = new List<string>();
                }
                if (!d.Equals("spacer"))
                    res[index+1].Add(d);
            }
        }

        // process moves
        while (moves.Any()) {
            var move = moves.Dequeue();
            foreach(var _ in Enumerable.Range(0, move.Count)) {
                var crate = res[move.From].First();
                res[move.From].RemoveAt(0);
                res[move.To].Insert(0, crate);
            }
        }

        var r = res.Select(r => r.Value.First().TrimStart('[').TrimEnd(']')).ToList();
        result = string.Join("", r);

        return result;
    }

    static string solveSecondProblem(string[] input) {
        var res = new Dictionary<int, List<string>>();
        var moves = new Queue<Move>();

        foreach(var line in input) {
            if (line.StartsWith("move")) {
                // movement
                var numbers = line.TrimEnd('\r').Split(" ").Where(s => Int32.TryParse(s, out _)).Select(s => Int32.Parse(s)).ToList();
                moves.Enqueue(new Move {
                    Count = numbers[0],
                    From = numbers[1],
                    To = numbers[2]
                });
                continue;
            }

            if (!line.Contains("[")) {
                // This is the number line
                continue;
            }

            if (line.Equals(String.Empty)) {
                // The line before move line
                continue;
            }

            // crates
            var data = ProcessLine(line);
            foreach(var (d, index) in data.WithIndex()) {
                if (!res.ContainsKey(index + 1)) {
                    res[index+1] = new List<string>();
                }
                if (!d.Equals("spacer"))
                    res[index+1].Add(d);
            }
        }

        // process moves
        while (moves.Any()) {
            var move = moves.Dequeue();
            var crates = res[move.From].GetRange(0, move.Count);
            foreach(var _ in Enumerable.Range(0, move.Count)) {
                // var crate = res[move.From].First();
                res[move.From].RemoveAt(0);
                // res[move.To].Insert(0, crate);
            }

            res[move.To].InsertRange(0, crates);
        }

        var r = res.Select(r => r.Value.First().TrimStart('[').TrimEnd(']')).ToList();
        var result = string.Join("", r);

        return result;
    }

    static IEnumerable<string> ProcessLine(string line) {

        var result = new List<string>();
        var currentString = "";
        var letterEnded = false;
        foreach(var c in line.TrimEnd('\r')) {
            if (letterEnded) {
                letterEnded = false;
                continue;
            }

            switch(c) {
                case ' ':
                    currentString += " ";
                    break;
                case '[':
                    currentString = "[";
                    break;
                case ']':
                    currentString += "]";
                    break;
                default:
                    // should be a letter
                    currentString += c;
                    break;
            }

            if (currentString == "   ") {
                result.Add("spacer");
                currentString = "";
                letterEnded = true;
            }

            if (currentString.EndsWith(']')) {
                result.Add(currentString);
                currentString = "";
                letterEnded = true;
            }
        }

        return result;
    }
}

class Move {
    public int Count { get; set; }
    public int From { get; set; }
    public int To  { get; set; }
}

public static class ListExtensions {
    public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> source)
    {
        return source.Select((item, index) => (item, index));
    }
}