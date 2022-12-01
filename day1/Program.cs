// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var input = await File.ReadAllLinesAsync("./input.txt");
var elfs = new List<Elf>();
var newElf = true;

foreach (var line in input)
{
    if (line.Trim().Equals(String.Empty)) {
        //next line is new elf
        newElf = true;
        continue;
    }
    if (newElf) {
        elfs.Add(new Elf());
        newElf = false;
    }

    var elf = elfs.Last();

    elf.Calories.Add(Int32.Parse(line));
}

// First challenge
var max = elfs.Max(e => e.CalorySum);
Console.WriteLine(max);

// Second challenge
var ordered = elfs.OrderByDescending(e => e.CalorySum).ToList();
var top3 = new List<Elf>
{
    ordered[0],
    ordered[1],
    ordered[2]
};

var top3Sum = top3.Sum(e => e.CalorySum);
Console.WriteLine(top3Sum);
