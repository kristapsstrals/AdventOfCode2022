internal class Program
{
    private static async Task Main(string[] args)
    {
        var testData = @"2-4,6-8
2-3,4-5
5-7,7-9
2-8,3-7
6-6,4-6
2-6,4-8";

        var testInput = testData.Split(Environment.NewLine);
        var input = await File.ReadAllLinesAsync("./input.txt");

        var firstTest = solveFirstProblem(testInput);
        if (firstTest != 2)
            throw new Exception($"Invalid result for first case: {firstTest}");
        var first = solveFirstProblem(input);

        var secondTest = solveSecondProblem(testInput);
        if (secondTest != 4)
            throw new Exception($"Invalid result for second case: {secondTest}");
        var second = solveSecondProblem(input);
        if (second >= 981)
            throw new Exception("result too high");
        Console.WriteLine($"First: {first}");
        Console.WriteLine($"Second: {second}");
    }

    static int solveFirstProblem(string[] input) {
        var result = 0;
        foreach(var line in input) {
            var data = line.Split(",");
            var firstNumbers = data[0].Trim().Split("-").Select(str => Int32.Parse(str)).ToList();
            var secondNumbers = data[1].Trim().Split("-").Select(str => Int32.Parse(str)).ToList();

            if ((firstNumbers[0] >= secondNumbers[0] && firstNumbers[1] <= secondNumbers[1]) ||
                (secondNumbers[0] >= firstNumbers[0] && secondNumbers[1] <= firstNumbers[1]))
            {
                result++;
            }
        }
        return result;
    }

    static int solveSecondProblem(string[] input) {
        var result = 0;

        foreach(var line in input) {
            var data = line.Split(",");
            var firstNumbers = data[0].Trim().Split("-").Select(str => Int32.Parse(str)).ToList();
            var secondNumbers = data[1].Trim().Split("-").Select(str => Int32.Parse(str)).ToList();

            var firstList = Enumerable.Range(firstNumbers[0], firstNumbers[1] - firstNumbers[0] + 1);
            var secondList = Enumerable.Range(secondNumbers[0], secondNumbers[1] - secondNumbers[0] + 1);
            var intersect = firstList.Intersect(secondList).ToList();
            if (intersect.Any()) {
                result++;
            }
        }
        return result;
    }
}