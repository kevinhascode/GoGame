using System.Collections.Generic;
using System.Linq;

namespace GoGame
{
    /// <summary>
    /// All of the logic about making moves are to be written here.
    /// Anything that's calculation-based or was written in any way as a convenience.
    /// </summary>
    public class GameLogic
    {
        public bool IsWhiteMove { get; set; }

        private GoGame Game;

        public GameLogic(GoGame game)
        {
            Reset(game);
        }

        public void Reset(GoGame game)
        {
            Game = game;
            IsWhiteMove = (game.Handicap > 0);
        }

        public void ChangeTurn()
        {
            IsWhiteMove = !IsWhiteMove;
        }

        // Checks on legality of move.
        public bool IsLegal(Loc proposedLoc)
        {
            // TODO: would like to return RequestResponse from here, but not sure how it should look
            if (isNotConflict(proposedLoc))
                return true;
            else
                return false;
        }

        /* The next best thing to a picture in the source code.
         
                   IsValid()
                      ||
                      ||
                    IsKill
                  n/      \y
             IsSuicide    IsKo
             n/    \y    n/  \y
            PLAY    NO  PLAY  NO
         
         */

        // TODO: from jotted down notes
        //public Move IsValid(Loc loc)
        //{
        //    List<Chain> chain = isKill(loc);

        //    if (chain != null)
        //        return IsKo(loc);
        //    else
        //        return IsSuicide(loc);
        //}

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

        private List<Chain> isKill()
        {
            /*
             * Iterate through chains of opposite color.
             * if any have 1 liberty, it's a kill and is returned in List<Chain>
             */
            return new List<Chain>();
        }

        // Updates lists accordingly with correct location and color.
        public RequestResponse PlaceStone(Loc loc)
        {
            // By this point, we have MergeResult and KilledChains
            this.Game.PossibleKoLoc = null;

            Stone stone = new Stone(loc, IsWhiteMove);

            List<Loc> liberties = findLiberties(stone.Loc);

            Chain chain = new Chain(stone, liberties);

            if (IsWhiteMove)
                this.addStoneToChains(this.Game.whiteChains, chain);
            else
                this.addStoneToChains(this.Game.blackChains, chain);

            this.ChangeTurn();

            return new RequestResponse(new Move(stone));
        }

        private List<Loc> findLiberties(Loc loc)
        {
            List<Loc> liberties = new List<Loc>();

            // Check Left.
            Loc left = new Loc(loc.X - 1, loc.Y);
            if (loc.X > 0 && !Game.LocsPlayed.Contains(left))
                liberties.Add(left);

            // Check Right.
            Loc right = new Loc(loc.X + 1, loc.Y);
            if (loc.X < Game.BoardSize - 1 && !Game.LocsPlayed.Contains(right))
                liberties.Add(right);

            // Check Up.
            Loc up = new Loc(loc.X, loc.Y - 1);
            if (loc.Y > 0 && !Game.LocsPlayed.Contains(up))
                liberties.Add(up);

            // Check Down.
            Loc down = new Loc(loc.X, loc.Y + 1);
            if (loc.Y < Game.BoardSize - 1 && !Game.LocsPlayed.Contains(down))
                liberties.Add(down);

            return liberties;
        }

        // By this point, the move is already deemed valid, this adds the stone to the logical stone collections.
        private void addStoneToChains(List<Chain> chainsOfLikeColor, Chain chainPlayed)
        {
            List<Chain> chainsToMerge = new List<Chain>();

            // for each chain of the same color with a liberty at the proposed location
            foreach (Chain chain in chainsOfLikeColor)
                foreach (Loc loc in chain.Liberties)
                    if (chainPlayed.Stones.First().Loc.Equals(loc))
                        chainsToMerge.Add(chain);

            // if there are matches, merge them all
            if (chainsToMerge.Count > 0)
            {
                foreach (Chain chain in chainsToMerge)
                    chainsOfLikeColor.Remove(chain);

                chainsOfLikeColor.Add(Chain.Merge(chainsToMerge));
            }
            else
                chainsOfLikeColor.Add(chainPlayed);
        }
    }
}