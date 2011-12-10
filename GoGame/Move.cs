using System.Collections.Generic;

namespace GoGame
{
    internal class Move
    {
        internal readonly Stone StonePlaced;
        internal readonly List<Chain> ChainsKilled;

        public Move(Stone stonePlaced)
        {
            this.StonePlaced = stonePlaced;
            this.ChainsKilled = new List<Chain>();
        }

        public Move(Stone stonePlaced, List<Chain> chainsKilled)
        {
            this.StonePlaced = stonePlaced;
            this.ChainsKilled = chainsKilled;
        }
    }
}