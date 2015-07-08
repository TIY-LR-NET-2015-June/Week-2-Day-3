using System;
using System.Collections.Generic;
using System.Linq;

namespace Week2Day3RPS
{
    public class RPSGame
    {
        public List<Option> RecordedUserMoves = new List<Option>();

        public static Dictionary<string, Result> PossibleCombos = new Dictionary<string, Result>()
            {
                { "PP", Result.Tie },
                { "RR", Result.Tie },
                { "SS", Result.Tie },
                { "RP", Result.AI },
                { "RS", Result.User },
                { "PR", Result.User },
                { "PS", Result.AI },
                { "SR", Result.AI },
                { "SP", Result.User },
            };

        public enum Option
        {
            Rock = 0,
            Paper = 1,
            Scissors = 2
        }

        public enum Result
        {
            AI,
            User,
            Tie
        }
        public static Option GetAIMove(List<Option> previousUserMoves)
        {
            Random randomGenerator = new Random();

            int pmCount = previousUserMoves.Count;

            if (pmCount <= 2)
            {
                var randomNumber = randomGenerator.Next(0, 3);
                return (Option)randomNumber;
            }

            int numMovesToLookBack = 10;
            if (numMovesToLookBack > pmCount)
                numMovesToLookBack = pmCount;

            var movesReversed = new List<Option>(previousUserMoves);
            movesReversed.Reverse();



            //FIND THE MODE
            var mostPlayed = movesReversed.GroupBy(x => x).OrderByDescending(g => g.Count()).First().Key;

            ///END FIND THE MODE

            var variance = randomGenerator.Next(-10, 10);
            if (mostPlayed == Option.Paper)
            {
                var percentage = randomGenerator.Next(1, 101);
                if (percentage + variance >= 40)
                    return Option.Scissors;
                else
                    return Option.Paper;
            }

            if (mostPlayed == Option.Rock)
            {
                var percentage = randomGenerator.Next(1, 101);
                if (percentage + variance >= 40)
                    return Option.Paper;
                else
                    return Option.Rock;
            }


            if (mostPlayed == Option.Scissors)
            {
                var percentage = randomGenerator.Next(1, 101);
                if (percentage + variance >= 40)
                    return Option.Rock;
                else
                    return Option.Scissors;
            }


            return Option.Paper;


        }

        public string Play(Option userPlay, Option computerPlay)
        {
            var move = userPlay.ToString().Substring(0, 1) + computerPlay.ToString().Substring(0, 1);

            Result result = PossibleCombos[move];

            RecordedUserMoves.Add(userPlay);
            return string.Format("User: {0}, AI: {1} ---- Results: {2}", userPlay, computerPlay, result);
        }

        public static bool TryParseMove(string input, out Option userPlay)
        {
            switch (input.ToLower())
            {
                case "r":
                case "rock":
                    userPlay = Option.Rock;
                    return true;

                case "s":
                case "scissors":
                    userPlay = Option.Scissors;
                    return true;

                case "p":
                case "paper":
                    userPlay = Option.Paper;
                    return true;
                default:
                    userPlay = Option.Paper;
                    return false;
            }
        }
    }
}