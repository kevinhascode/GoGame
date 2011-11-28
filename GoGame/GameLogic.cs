using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoGame
{
    /// <summary>
    /// All of the logic about making moves are to be written here.
    /// Anything that's calculation-based or was written in any way as a convenience.
    /// </summary>
    internal class GameLogic
    {
        internal static bool IsWhiteMove { get; set; }

        private GoGame Game;

        public GameLogic(GoGame game)
        {
            this.Game = game;
        }

        internal static void Reset(GoGame game)
        {
            IsWhiteMove = (game.Handicap > 0);
        }

        internal static void ChangeTurn()
        {
            IsWhiteMove = !IsWhiteMove;
        }

        // Checks on legality of move.
        internal bool IsLegal(Loc proposedLoc)
        {
            // TODO: would like to return RequestResponse from here, but not sure how it should look
            if (isNotConflict(proposedLoc))
                return true;
            else
                return false;
        }

        private bool isNotConflict(Loc proposedLoc)
        {
            return this.isNotConflictHelper(this.Game.blackChains, proposedLoc)
                && this.isNotConflictHelper(this.Game.whiteChains, proposedLoc);
        }

        private bool isNotConflictHelper(List<Chain> chainsOfLikeColor, Loc proposedLoc)
        {
            foreach (Chain chain in chainsOfLikeColor)
                foreach (Stone stone in chain.Stones)
                    if (proposedLoc.Equals(stone.Loc))
                        return false;

            return true;
        }

        // Updates lists accordingly with correct location and color.
        internal RequestResponse PlaceStone(Loc loc)
        {
            Stone stone = new Stone(loc, IsWhiteMove);

            if (IsWhiteMove)
                this.addStoneToChains(this.Game.whiteChains, stone);
            else
                this.addStoneToChains(this.Game.blackChains, stone);

            GameLogic.ChangeTurn();

            return new RequestResponse(new Move(stone));
        }

        // By this point, the move is already deemed valid, this adds the stone to the logical stone collections.
        private void addStoneToChains(List<Chain> chainsOfLikeColor, Stone stone)
        {
            List<Chain> chainsToMerge = new List<Chain>();

            // for each chain of the same color with a liberty at the proposed location
            foreach (Chain chain in chainsOfLikeColor)
                foreach (Loc loc in chain.Liberties)
                    if (stone.Loc.Equals(loc))
                        chainsToMerge.Add(chain);

            // if there are matches, merge them all
            if (chainsToMerge.Count > 0)
            {
                foreach (Chain chain in chainsToMerge)
                    chainsOfLikeColor.Remove(chain);

                chainsOfLikeColor.Add(Chain.Merge(chainsToMerge));
            }
            else
                chainsOfLikeColor.Add(new Chain(stone));
        }
    }
}