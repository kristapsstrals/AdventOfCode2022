internal class Program
{
    static Dictionary<char, int> priorities = new()
    {
        {'a', 1 },
        {'b', 2 },
        {'c', 3 },
        {'d', 4 },
        {'e', 5 },
        {'f', 6 },
        {'g', 7 },
        {'h', 8 },
        {'i', 9 },
        {'j', 10 },
        {'k', 11 },
        {'l', 12 },
        {'m', 13 },
        {'n', 14 },
        {'o', 15 },
        {'p', 16 },
        {'q', 17 },
        {'r', 18 },
        {'s', 19 },
        {'t', 20 },
        {'u', 21 },
        {'v', 22 },
        {'w', 23 },
        {'x', 24 },
        {'y', 25 },
        {'z', 26 },
        {'A', 27 },
        {'B', 28 },
        {'C', 29 },
        {'D', 30 },
        {'E', 31 },
        {'F', 32 },
        {'G', 33 },
        {'H', 34 },
        {'I', 35 },
        {'J', 36 },
        {'K', 37 },
        {'L', 38 },
        {'M', 39 },
        {'N', 40 },
        {'O', 41 },
        {'P', 42 },
        {'Q', 43 },
        {'R', 44 },
        {'S', 45 },
        {'T', 46 },
        {'U', 47 },
        {'V', 48 },
        {'W', 49 },
        {'X', 50 },
        {'Y', 51 },
        {'Z', 52 }
    };

    private static async Task Main(string[] args)
    {
        var testData = @"vJrwpWtwJgWrhcsFMMfFFhFp
jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
PmmdzqPrVvPwwTWBwg
wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
ttgJtRGJQctTZtZT
CrZsJsPPZsGzwwsLwLmpwMDw";

        var input = testData.Split(Environment.NewLine);
        input = await File.ReadAllLinesAsync("./input.txt");

        var part1Answer = GetPart1Answer(input);
        var part2Answer = GetPart2Answer(input);

        Console.WriteLine($"Part 1 answer: {part1Answer}");
        Console.WriteLine($"Part 2 answer: {part2Answer}");
    }

    static int GetPart1Answer(string[] input)
    {
        var rucksacks = input.Select(line =>
        {
            var half = (line.Length) / 2;
            var sanitized = line.TrimEnd('\r');
            var rucksack = new Rucksack
            {
                FirstCompartment = sanitized.Take(half).ToList(),
                SecondCompartment = sanitized.Skip(half).ToList()
            };
            return rucksack;
        });

        var part1Answer = rucksacks.Sum(r => r.PrioritySum);
        return part1Answer;
    }

    static int GetPart2Answer(string[] input)
    {
        var rucksacks = input.Select(line =>
        {
            var half = (line.Length) / 2;
            var sanitized = line.TrimEnd('\r');
            var rucksack = new Rucksack
            {
                FirstCompartment = sanitized.Take(half).ToList(),
                SecondCompartment = sanitized.Skip(half).ToList()
            };
            return rucksack;
        });

        var split = rucksacks.Select((c, index) => new { c, index })
            .GroupBy(x => x.index / 3)
            .Select(group => group.Select(elem => elem.c));

        int sum = 0;

        foreach (var group in split)
        {
            var listOfLists = group.Select(r =>
            {
                var returnVal = new List<char>(r.FirstCompartment);
                returnVal.AddRange(r.SecondCompartment);
                return returnVal;
            }).ToList();

            var intersection = listOfLists
                .Skip(1)
                .Aggregate(
                    new HashSet<char>(listOfLists.First()),
                    (h, e) => { h.IntersectWith(e); return h; }
                ).First();

            sum += priorities[intersection];
        }

        return sum;
    }

    class Rucksack
    {
        public List<char> FirstCompartment { get; set; }
        public List<char> SecondCompartment { get; set; }

        public IEnumerable<char> CommonValues =>
            FirstCompartment.Intersect(SecondCompartment);

        public int PrioritySum => CommonValues.Select(v => priorities[v]).Sum();
    }
}
