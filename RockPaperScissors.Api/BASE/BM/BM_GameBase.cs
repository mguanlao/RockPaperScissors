using RockPaperScissors.Api.DTO;
using RockPaperScissors.Api.ENUM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors.Api.BASE.BM
{
    public abstract class BM_GameBase
    {
        #region Properties
        private const int WinCondition = 3;
        public DTO_Game Game { get; set; }
        public int RoundNumber { get; set; }
        private List<DTO_PlayerAction> PlayerActions;
        #endregion

        #region Constructors
        public BM_GameBase()
        {
            RoundNumber = 0;
            InitializePlayerActions();
        }

        public BM_GameBase(List<DTO_Player> players)
        {
            Game = new DTO_Game(players);
            RoundNumber = 0;
            InitializePlayerActions();
        }
        #endregion

        #region Methods
        public abstract DTO_Player PlayGame(PlayerAction player1Action, PlayerAction player2Action = PlayerAction.None);

        /// <summary>
        /// Saves the players actions per round in a Dictionary
        /// </summary>
        /// <param name="player"></param>
        /// <param name="playerAction"></param>
        /// <param name="roundNumber"></param>
        public void SetPlayerActions(DTO_Player player, PlayerAction playerAction, int roundNumber)
        {
            player.PlayerActions.Add(roundNumber, playerAction);
        }

        /// <summary>
        /// Determines the winning player based on the action of each player
        /// </summary>
        /// <param name="player1Action"></param>
        /// <param name="player2Action"></param>
        /// <returns>Returns the winning player, returns null if tied</returns>
        public DTO_Player DetermineWinner(PlayerAction player1Action, PlayerAction player2Action)
        {
            if (player1Action == player2Action)
            {
                return null;
            }
            else
            {
                if (PlayerActions.Where(action => action.PlayerAction == player1Action)
                    .Any(action2 => action2.LosesAgainst.Contains(player2Action)))
                {
                    Game.Players[1].WinCount++;
                    return Game.Players[1];
                }
                else
                {
                    Game.Players[0].WinCount++;
                    return Game.Players[0];
                }
            }
        }

        /// <summary>
        /// Checks if a player has already won the game based on the number of won rounds
        /// </summary>
        public void CheckIfAPlayerHasWon()
        {
            if (Game.Players.Any(p => p.WinCount == WinCondition))
            {
                Game.HasWinner = true;
            }
        }

        /// <summary>
        /// Determines what the computer action will be based on the computer type.
        /// Normal computer will return the action that will beat the previous move.
        /// Randomized computer will return a random action.
        /// </summary>
        /// <returns>Returns the computer action</returns>
        public PlayerAction DetermineComputerAction()
        {
            if (Game.Players[1].PlayerType == PlayerType.Computer)
            {
                // Check previous move
                var previousMove = Game.Players[1].PlayerActions.OrderByDescending(p => p.Key).FirstOrDefault().Value;

                if (previousMove == PlayerAction.None)
                {
                    return GetRandomizedPlayerAction();
                }
                else
                {
                    return GetActionFromPreviousMove(previousMove);
                }
            }
            else
            {
                return GetRandomizedPlayerAction();
            }
        }

        /// <summary>
        /// Gets a random player action
        /// </summary>
        /// <returns>Returns the player action</returns>
        private PlayerAction GetRandomizedPlayerAction()
        {
            Random random = new Random();
            return PlayerActions[random.Next((int)PlayerAction.Rock, PlayerActions.Count)].PlayerAction;
        }

        /// <summary>
        /// Gets a player action which will beat the passed player action
        /// </summary>
        /// <param name="previousAction"></param>
        /// <returns>Returns a player action based on the passed player action</returns>
        private PlayerAction GetActionFromPreviousMove(PlayerAction previousAction)
        {
            Random random = new Random();

            return PlayerActions.Where(action => action.PlayerAction == previousAction)
                .Select(action2 => action2.LosesAgainst[random.Next(0, action2.LosesAgainst.Count)])
                .FirstOrDefault();
            //switch (previousAction)
            //{
            //    case PlayerAction.Rock:
            //        return PlayerAction.Paper;

            //    case PlayerAction.Paper:
            //        return PlayerAction.Scissor;

            //    case PlayerAction.Scissor:
            //        return PlayerAction.Rock;

            //    default:
            //        return PlayerAction.None;
            //}
        }

        /// <summary>
        /// Initializes available player actions here.
        /// Add any new player action here.
        /// </summary>
        private void InitializePlayerActions()
        {
            PlayerActions = new List<DTO_PlayerAction>();

            PlayerActions.Add(new DTO_PlayerAction(PlayerAction.Rock, new List<PlayerAction> { PlayerAction.Paper }));
            PlayerActions.Add(new DTO_PlayerAction(PlayerAction.Paper, new List<PlayerAction> { PlayerAction.Scissor, PlayerAction.Flamethrower }));
            PlayerActions.Add(new DTO_PlayerAction(PlayerAction.Scissor, new List<PlayerAction> { PlayerAction.Rock }));
            PlayerActions.Add(new DTO_PlayerAction(PlayerAction.Flamethrower, new List<PlayerAction> { PlayerAction.Rock, PlayerAction.Scissor }));
        }
    }
    #endregion
}
