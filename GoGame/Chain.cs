using System.Collections.Generic;
using System.Linq;

namespace GoGame
{
    internal class Chain
    {
        internal readonly List<Stone> Stones;
        internal List<Loc> Liberties { get; set; }

        internal bool IsWhite
        {
            get { return this.Stones.First().IsWhite; }
        }

        public Chain()
        {
            this.Stones = new List<Stone>();
            this.Liberties = new List<Loc>();
        }

        public Chain(Stone stone)
            : this()
        {
            this.Stones.Add(stone);
        }

        // Convenience method for merging multiple chains
        internal static Chain Merge(List<Chain> chains)
        {
            Chain toReturn = new Chain();

            foreach (Chain chain in chains)
            {
                toReturn.Stones.AddRange(chain.Stones);
                toReturn.Liberties = toReturn.Liberties.Union(chain.Liberties).ToList<Loc>();
            }

            return toReturn;
        }
    }
}