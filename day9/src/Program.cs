﻿namespace day9;

public class Program
{
    private static async Task Main(string[] args)
    {
        var input = await File.ReadAllLinesAsync("./input.txt");
        var first = SolveProblem(input, 1);
        var second = SolveProblem(input, 9);

        Console.WriteLine($"First: {first}");
        Console.WriteLine($"Second: {second}");
    }

    public static int SolveProblem(string[] input, int tailCount) {
        var trimmedLines = input.Select(l => l.Trim().TrimEnd('\r'));

        var headPosition = new Coordinate() { X = 0, Y = 0 };
        var tailPositions = new List<Coordinate>();
        for (int i = 0; i < tailCount; i++)
        {
            tailPositions.Add(new Coordinate() { X = 0, Y = 0 });
        }
        var tailVisitedPositions = new List<Coordinate>() {
            new Coordinate() { X = 0, Y = 0 }
        };

        foreach(var line in trimmedLines) {
            var split = line.Split(" ");
            var direction = split[0];
            var steps = Int32.Parse(split[1]);
            for (int i = 0; i < steps; i++)
            {    
                MoveCoordinate(headPosition, direction);

                for (int tailIndex = 0; tailIndex < tailPositions.Count; tailIndex++)
                {
                    var tailPosition = tailPositions[tailIndex];
                    var compareCoordinate = tailIndex == 0 ? headPosition : tailPositions[tailIndex - 1];
                    var distanceX = compareCoordinate.X - tailPosition.X;
                    var distanceY = compareCoordinate.Y - tailPosition.Y;

                    if (Math.Abs(distanceX) > 1 || Math.Abs(distanceY) > 1) {
                        if (distanceX != 0 && tailPosition.X > compareCoordinate.X)
                            tailPosition.X--;
                        if (distanceX != 0 && tailPosition.X < compareCoordinate.X)
                            tailPosition.X++;

                        if (distanceY != 0 && tailPosition.Y > compareCoordinate.Y)
                            tailPosition.Y--;
                        if (distanceY != 0 && tailPosition.Y < compareCoordinate.Y)
                            tailPosition.Y++;

                        // MoveCoordinate(tailPosition, direction);
                        if (tailIndex == tailPositions.Count - 1) {
                            if (!tailVisitedPositions.Any(p => p.X == tailPosition.X && p.Y == tailPosition.Y))
                                tailVisitedPositions.Add(
                                    new Coordinate() { X = tailPosition.X, Y = tailPosition.Y }
                                );
                        }
                    }
                }
            }
        }

        return tailVisitedPositions.Count;
    }

    static void MoveCoordinate(Coordinate coordinate, string direction) {
        switch(direction) {
            case "U":
                coordinate.Y++;
                break;
            case "D":
                coordinate.Y--;
                break;
            case "L":
                coordinate.X--;
                break;
            case "R":
                coordinate.X++;
                break;
        }
    }
}

public class Coordinate
{
    public long X { get; set; }
    public long Y { get; set; }
}