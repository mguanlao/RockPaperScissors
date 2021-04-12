using RockPaperScissors.Api.ENUM;
using System.Collections.Generic;

namespace RockPaperScissors.Api.DTO
{
    public class DTO_Player
    {
        #region Properties
        public Dictionary<int, PlayerAction> PlayerActions { get; set; }
        public int WinCount { get; set; }
        public int PlayerNumber { get; set; }
        public PlayerType PlayerType { get; set; }
        #endregion

        #region Constructors
        public DTO_Player()
        {
            PlayerActions = new Dictionary<int, PlayerAction>();
        }

        public DTO_Player(int playerNumber, PlayerType playerType)
        {
            PlayerNumber = playerNumber;
            PlayerType = playerType;
            PlayerActions = new Dictionary<int, PlayerAction>();
        }
        #endregion
    }
}
