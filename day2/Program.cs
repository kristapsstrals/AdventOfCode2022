internal class Program
{
    private static async Task Main(string[] args)
    {
var testData = @"A Y
B X
C Z";

        var input = testData.Split(Environment.NewLine);
        input = await File.ReadAllLinesAsync("./input.txt");

        var firstResult = First.Run(input);
        Console.WriteLine($"First result: {firstResult}");
        var secondResult = Second.Run(input);
        Console.WriteLine($"Second result: {secondResult}");
    }
}
