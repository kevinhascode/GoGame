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

        /* The next best thing to a picture in the source code.
         
                       IsLegal()
                          ||
                          ||
                        IsKill
                      n/      \y
                 IsSuicide    IsKo
                 n/    \y    n/  \y
                PLAY    NO  PLAY  NO
         
         */
        public IsLegalResponse IsLegal(Loc proposedLoc)
        {
            IsLegalResponse response = new IsLegalResponse();

            if (isConflict(proposedLoc))
            {
                response.Reason = ReasonEnum.Conflict;
                return response;
            }

            response.Killed = determineKills(proposedLoc);

            if (response.Killed.Any())
            {   // IsKill
                if (isKo(proposedLoc, response.Killed))
                {
                    response.Reason = ReasonEnum.Ko;
                    return response;
                }
                else
                {   // Determine MergeResultant and AbsorbedByMerge for future use.
                    // Does proposedLoc have any friendly neighbors?
                    List<Chain> friendlyNeighborChains = findFriendlyNeighborChains(proposedLoc);

                    if (friendlyNeighborChains.Any())
                    {
                        // If so, merge
                        friendlyNeighborChains.Add(new Chain(new Stone(proposedLoc, IsWhiteMove),
                                                             this.findLiberties(proposedLoc)));
                        response.MergeResultant = this.merge(friendlyNeighborChains);
                        response.AbsorbedInMerge = friendlyNeighborChains;
                    }

                    return response;
                }
            }
            else
            {   // !IsKill
                this.determineSuicide(proposedLoc, response);

                return response;
            }
        }

        private bool isConflict(Loc proposedLoc)
        {
            return this.isConflictHelper(this.Game.blackChains, proposedLoc)
                || this.isConflictHelper(this.Game.whiteChains, proposedLoc);
        }

        private bool isConflictHelper(List<Chain> chainsOfLikeColor, Loc proposedLoc)
        {
            foreach (Chain chain in chainsOfLikeColor)
                foreach (Stone stone in chain.Stones)
                    if (proposedLoc.Equals(stone.Loc))
                        return true;

            return false;
        }

        // TODO: Can also do a quick scan of neighbors if there are any foes, so see if even need to look for kills.
        private List<Chain> determineKills(Loc proposedLoc)
        {
            List<Chain> kills = new List<Chain>();

            // Iterate through chains of opposite color.
            // if any have 1 liberty, it's a kill and is returned in List<Chain>
            if (IsWhiteMove)
                foreach (Chain chain in Game.blackChains)
                {
                    if (chain.Liberties.Count == 1 && chain.Liberties[0].Equals(proposedLoc))
                        kills.Add(chain);
                }
            else
                foreach (Chain chain in Game.whiteChains)
                {
                    if (chain.Liberties.Count == 1 && chain.Liberties[0].Equals(proposedLoc))
                        kills.Add(chain);
                }

            return kills;
        }

        private void determineSuicide(Loc proposedLoc, IsLegalResponse response)
        {
            // Does proposedLoc have any friendly neighbors?
            List<Chain> friendlyNeighborChains = findFriendlyNeighborChains(proposedLoc);

            if (friendlyNeighborChains.Any())
            {
                // If so, merge
                friendlyNeighborChains.Add(new Chain(new Stone(proposedLoc, IsWhiteMove),
                    this.findLiberties(proposedLoc)));
                Chain mergeResultant = this.merge(friendlyNeighborChains);

                if (!mergeResultant.Liberties.Any())
                {
                    response.Reason = ReasonEnum.Suicide;
                    return;
                }
                else
                {
                    response.MergeResultant = mergeResultant;
                    response.AbsorbedInMerge = friendlyNeighborChains;
                    return;
                }
            }
            else
            {
                // If not, if there are any liberties, it's playable
                if (this.findLiberties(proposedLoc).Any())
                {
                    response.MergeResultant = new Chain(new Stone(proposedLoc, IsWhiteMove),
                        this.findLiberties(proposedLoc));
                    response.AbsorbedInMerge = new List<Chain>();
                    return;
                }
            }

            // else, it's suicide
            response.Reason = ReasonEnum.Suicide;
        }

        private bool isKo(Loc proposedLoc, List<Chain> killed)
        {
            return proposedLoc.Equals(Game.PossibleKoLoc)
                && killed.Count == 1
                && killed[0].Stones.Count == 1;
        }

        private List<Chain> findFriendlyNeighborChains(Loc loc)
        {
            List<Chain> friendlyChains = new List<Chain>();

            if (IsWhiteMove)
                foreach (Chain chain in Game.whiteChains)
                    foreach (Loc liberty in chain.Liberties)
                    {
                        if (liberty.Equals(loc))
                            friendlyChains.Add(chain);
                    }
            else
                foreach (Chain chain in Game.blackChains)
                    foreach (Loc liberty in chain.Liberties)
                    {
                        if (liberty.Equals(loc))
                            friendlyChains.Add(chain);
                    }

            return friendlyChains;
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

        private void reCalcLiberties(Chain chain)
        {
            List<Loc> liberties = new List<Loc>();

            foreach (Stone stone in chain.Stones)
                liberties = liberties.Union(findLiberties(stone.Loc)).ToList();

            chain.Liberties = liberties;
        }

        private Chain merge(List<Chain> chainsToMerge)
        {
            Chain mergeResultant = new Chain();

            foreach (Chain chain in chainsToMerge)
            {
                mergeResultant.Stones.AddRange(chain.Stones);
                mergeResultant.Liberties = mergeResultant.Liberties.Union(chain.Liberties).ToList();
            }

            foreach (Stone stone in mergeResultant.Stones)
                mergeResultant.Liberties.Remove(stone.Loc);

            return mergeResultant;
        }

        // Updates lists accordingly with correct location and color.
        public RequestResponse PlaceStone(Loc loc, IsLegalResponse isLegalResponse)
        {
            // By this point, we have MergeResult and KilledChains
            this.Game.PossibleKoLoc = new Loc(-1, -1);

            // Update logical groups of Chains by adding the mergeResult and removing what went into the merge
            this.enactMerge(isLegalResponse.MergeResultant, isLegalResponse.AbsorbedInMerge);
            // Update logical groups of Chains by removing the killed ones.
            this.deadifyKilledChains(isLegalResponse.Killed);

            if (IsWhiteMove)
            {
                this.Game.PrisonersTakenByWhite += isLegalResponse.Killed.Sum(chain => chain.Stones.Count);

                foreach (Chain chain in this.Game.whiteChains)
                    this.reCalcLiberties(chain);
            }
            else
            {
                this.Game.PrisonersTakenByBlack += isLegalResponse.Killed.Sum(chain => chain.Stones.Count);

                foreach (Chain chain in this.Game.blackChains)
                    this.reCalcLiberties(chain);
            }

            // Update logical groups of Opponent's Chains by removing breath for current Loc);)
            this.takeMyBreathAwaaaaay(loc);

            // Set possibleKoLoc. ... TODO: unfuck Ko Logic
            if (isLegalResponse.Killed.Count == 1
                && isLegalResponse.Killed[0].Stones.Count == 1)
            {
                this.Game.PossibleKoLoc = isLegalResponse.Killed[0].Stones[0].Loc;
            }

            RequestResponse response = new RequestResponse(new Move(new Stone(loc, IsWhiteMove), isLegalResponse.Killed));

            this.ChangeTurn();

            return response;
        }

        // Actually changes logical game objects to impact game-state
        private void enactMerge(Chain chainToAdd, List<Chain> chainsToRemove)
        {
            // Remove chains.
            if (IsWhiteMove)
                foreach (Chain chain in chainsToRemove)
                    Game.whiteChains.Remove(chain);
            else
                foreach (Chain chain in chainsToRemove)
                    Game.blackChains.Remove(chain);

            // Add new chain.
            if (IsWhiteMove)
                Game.whiteChains.Add(chainToAdd);
            else
                Game.blackChains.Add(chainToAdd);
        }

        private void deadifyKilledChains(List<Chain> zombies)
        {
            if (IsWhiteMove)
                foreach (Chain deadChain in zombies)
                    Game.blackChains.Remove(deadChain);
            else
                foreach (Chain deadChain in zombies)
                    Game.whiteChains.Remove(deadChain);
        }

        private void takeMyBreathAwaaaaay(Loc loc)
        {
            if (IsWhiteMove)
                foreach (Chain chain in Game.blackChains)
                    chain.Liberties.Remove(loc);
            else
                foreach (Chain chain in Game.whiteChains)
                    chain.Liberties.Remove(loc);
        }
    }
}