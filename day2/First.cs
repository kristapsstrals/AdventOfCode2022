internal class First
{
    public static int Run(string[] input)
    {
        var scores = new List<int>();

        foreach (var line in input)
        {
            var moves = line.Split(" ");
            if (moves.Length != 2)
            {
                Console.WriteLine($"Error? Line: ${line}");
                continue;
            }

            var opponentMove = moves[0].Trim();
            var myMove = moves[1].Trim();

            var score = CalculateScore(myMove, opponentMove);
            scores.Add(score);
        }

        return scores.Sum();
    }

    static int CalculateScore(string myMove, string opponentMove)
        {
            int score = 0;
            // move score
            switch (myMove)
            {
                case MyRockPaperScissors.Rock:
                    score += 1;
                    break;
                case MyRockPaperScissors.Paper:
                    score += 2;
                    break;
                case MyRockPaperScissors.Scissors:
                    score += 3;
                    break;
            }

            // tie
            if (myMove == MyRockPaperScissors.Rock && opponentMove == OpponentRockPaperScissors.Rock ||
                myMove == MyRockPaperScissors.Paper && opponentMove == OpponentRockPaperScissors.Paper ||
                myMove == MyRockPaperScissors.Scissors && opponentMove == OpponentRockPaperScissors.Scissors)
            {
                score += 3;
                return score;
            }

            // won
            if (myMove == MyRockPaperScissors.Rock && opponentMove == OpponentRockPaperScissors.Scissors ||
                myMove == MyRockPaperScissors.Paper && opponentMove == OpponentRockPaperScissors.Rock ||
                myMove == MyRockPaperScissors.Scissors && opponentMove == OpponentRockPaperScissors.Paper)
            {

                score += 6;
                return score;
            }

            // lost
            return score;
        }
    static class OpponentRockPaperScissors {
        public const string Rock = "A";
        public const string Paper = "B";
        public const string Scissors = "C";
    }

    static class MyRockPaperScissors {
        public const string Rock = "X";
        public const string Paper = "Y";
        public const string Scissors = "Z";
    }
}

