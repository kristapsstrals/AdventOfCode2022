internal class Second
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
            var roundResult = moves[1].Trim();

            var score = CalculateScore(roundResult, opponentMove);
            scores.Add(score);
        }

        return scores.Sum();
    }

    static int CalculateScore(string roundResult, string opponentMove)
        {
            int score = 0;
            string myMove = String.Empty;
            // move score
            switch (roundResult)
            {
                case RoundResult.Lose:
                    switch(opponentMove) {
                        case RockPaperScissors.Rock:
                            myMove = RockPaperScissors.Scissors;
                            break;
                        case RockPaperScissors.Paper:
                            myMove = RockPaperScissors.Rock;
                            break;
                        case RockPaperScissors.Scissors:
                            myMove = RockPaperScissors.Paper;
                            break;
                    }
                    score += 0;
                    break;
                case RoundResult.Draw:
                    myMove = opponentMove;
                    score += 3;
                    break;
                case RoundResult.Win:
                    switch(opponentMove) {
                        case RockPaperScissors.Rock:
                            myMove = RockPaperScissors.Paper;
                            break;
                        case RockPaperScissors.Paper:
                            myMove = RockPaperScissors.Scissors;
                            break;
                        case RockPaperScissors.Scissors:
                            myMove = RockPaperScissors.Rock;
                            break;
                    }
                    score += 6;
                    break;
            }

            switch (myMove)
            {
                case RockPaperScissors.Rock:
                    score += 1;
                    break;
                case RockPaperScissors.Paper:
                    score += 2;
                    break;
                case RockPaperScissors.Scissors:
                    score += 3;
                    break;
            }

            return score;
        }
    static class RockPaperScissors {
        public const string Rock = "A";
        public const string Paper = "B";
        public const string Scissors = "C";
    }

    static class RoundResult {
        public const string Lose = "X";
        public const string Draw = "Y";
        public const string Win = "Z";
    }
}

