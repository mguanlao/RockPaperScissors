using RockPaperScissors.Api.BASE.BM;
using RockPaperScissors.Api.BM;
using RockPaperScissors.Api.DTO;
using RockPaperScissors.Api.ENUM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Rock Paper Scissors Game");
            string anotherGame = string.Empty;

            do
            {
                try
                {
                    anotherGame = StartGame();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error occurred: {ex.Message}");
                    Console.WriteLine("");
                }
            }
            while (anotherGame.ToUpper() == "Y");
        }

        private static string StartGame()
        {          
            // Get game mode based on the user input
            GameMode gameMode = GetGameMode();

            // Get appropriate BM class based on the game mode
            BM_GameBase game = BM_GameManager.StartGame(gameMode);

            // Keep playing while game has no winner
            while (!game.Game.HasWinner)
            {
                DTO_Player winner;

                if (gameMode == GameMode.HumanVsHuman)
                    winner = game.PlayGame(GetAction(1), GetAction(2));
                else
                    winner = game.PlayGame(GetAction(1));

                Console.WriteLine($"Game Round {game.RoundNumber}: ");

                // Display round winner
                if (winner == null)
                {
                    Console.WriteLine($"\t Player 1 action - {game.Game.Players[0].PlayerActions[game.RoundNumber]} vs " +
                        $"{(game.Game.Players[1].PlayerType != PlayerType.Human ? "Computer" : "Player 2")} action - " +
                        $"{game.Game.Players[1].PlayerActions[game.RoundNumber]}");
                    
                    Console.WriteLine($"\t Round {game.RoundNumber} resulted in a tie!");
                }
                else
                {
                    Console.WriteLine($"\t Player 1 action - {game.Game.Players[0].PlayerActions[game.RoundNumber]} vs " +
                        $"{(game.Game.Players[1].PlayerType != PlayerType.Human ? "Computer" : "Player 2")} action - " +
                        $"{game.Game.Players[1].PlayerActions[game.RoundNumber]} ");

                    Console.WriteLine($"\t Round {game.RoundNumber} - {(winner.PlayerType != PlayerType.Human ? "Computer" : "Player" + winner.PlayerNumber)} wins!");
                }

                // Display score tally
                Console.WriteLine("");
                Console.WriteLine($"Current score: Player 1 - {game.Game.Players[0].WinCount} wins | " +
                    $"{(game.Game.Players[1].PlayerType != PlayerType.Human ? "Computer" : "Player 2")} - {game.Game.Players[1].WinCount} wins.");
            }

            // Display game winner
            Console.WriteLine("");
            Console.WriteLine($@"{(game.Game.Players.OrderByDescending(p => p.WinCount).FirstOrDefault().PlayerType != PlayerType.Human ? 
                "Computer" : "Player " + game.Game.Players.OrderByDescending(p => p.WinCount).FirstOrDefault().PlayerNumber)} wins the game!");

            Console.WriteLine("");
            Console.WriteLine("Do you want to play another game? Y/N");
            return Console.ReadLine();
        }

        private static string GetGameModeInput()
        {
            Console.WriteLine("");
            Console.WriteLine("Choose a game mode:");
            Console.WriteLine("\t [1] For Single Player");
            Console.WriteLine("\t [2] For Two Players");

            return Console.ReadLine();
        }

        private static string GetComputerTypeInput()
        {
            Console.WriteLine("");
            Console.WriteLine("Choose a computer type:");
            Console.WriteLine("\t [1] For Normal");
            Console.WriteLine("\t [2] For Randomized");

            return Console.ReadLine();
        }

        private static GameMode GetGameMode()
        {
            string gameMode = string.Empty;
            string computerType = string.Empty;

            do
            {
                gameMode = GetGameModeInput();

                if (gameMode != "1" && gameMode != "2")
                {
                    Console.WriteLine("Invalid game mode.");
                }
            }
            while (gameMode != "1" && gameMode != "2");

            if (gameMode == "1")
            {
                do
                {
                    computerType = GetComputerTypeInput();

                    if (computerType != "1" && computerType != "2")
                    {
                        Console.WriteLine("Invalid computer type.");
                    }
                }
                while (computerType != "1" && computerType != "2");
            }

            if (gameMode == "2")
            {
                Console.WriteLine("");
                Console.WriteLine("Game mode is Human vs Human.");
                Console.WriteLine("Commencing game...");

                return GameMode.HumanVsHuman;
            }
            else
            {
                if (computerType == "1")
                {
                    Console.WriteLine("");
                    Console.WriteLine("Game mode is Human vs Computer.");
                    Console.WriteLine("Commencing game...");

                    return GameMode.HumanVsComputer;
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("Game mode is Human vs Computer (random logic).");
                    Console.WriteLine("Commencing game...");

                    return GameMode.HumanVsRandomizedComputer;
                }
            }
        }

        private static string GetActionInput(int player)
        {
            Console.WriteLine("");
            Console.WriteLine($"Player {player} choose your action:");
            Console.WriteLine("\t[R] Rock");
            Console.WriteLine("\t[P] Paper");
            Console.WriteLine("\t[S] Scissors");
            Console.WriteLine("\t[F] Flamethrower");

            return Console.ReadLine();
        }

        private static PlayerAction GetAction(int player)
        {
            string playerAction = string.Empty;

            do
            {
                playerAction = GetActionInput(player);
                playerAction = playerAction.ToUpper();

                if (playerAction != "R" && playerAction != "P" && playerAction != "S" && playerAction != "F")
                {
                    Console.WriteLine("Invalid action.");
                }
            }
            while (playerAction != "R" && playerAction != "P" && playerAction != "S" && playerAction != "F");

            if (playerAction == "R")
            {
                return PlayerAction.Rock;
            }
            else if (playerAction == "P")
            {
                return PlayerAction.Paper;
            }
            else if (playerAction == "S")
            {
                return PlayerAction.Scissor;
            }
            else if (playerAction == "F")
            {
                return PlayerAction.Flamethrower;
            }
            else
            {
                return PlayerAction.None;
            }
        }
    }
}
