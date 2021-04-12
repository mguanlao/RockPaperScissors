using System.Collections.Generic;

namespace RockPaperScissors.Api.DTO
{
    public class DTO_GameRound
    {
        #region Properties
        public Dictionary <int, DTO_Player> RoundWinner { get; set; }
        #endregion

        #region Constructors
        public DTO_GameRound()
        {
            RoundWinner = new Dictionary<int, DTO_Player>();
        }
        #endregion
    }
}
