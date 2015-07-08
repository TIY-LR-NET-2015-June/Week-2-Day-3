using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week2Day3RPS
{
    class Program
    {
        static void Main(string[] args)
        {
            RPSGame game = new RPSGame();
            bool IsGameOver = false;
            while (!IsGameOver)
            {
                Console.Write("Let's Play! Choose: (R)ock, (P)aper, (S)cissors.");

                RPSGame.Option userPlay;
                if (!RPSGame.TryParseMove(Console.ReadLine(), out userPlay))
                {
                    continue;
                }


                RPSGame.Option computerPlay = RPSGame.GetAIMove(game.RecordedUserMoves);
                string playResult = game.Play(userPlay, computerPlay); //human or computer or tie
                Console.WriteLine("Result: {0}", playResult);

                Console.WriteLine("Play again (y/n)?");
                string playAgain = Console.ReadLine();
                IsGameOver = playAgain == "n";
            }
        }
    }


}
