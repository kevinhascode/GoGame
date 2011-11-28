using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoGame
{
    // TODO: implement liberties
    internal class Chain
    {
        internal List<Stone> Stones { get; set; }
        internal List<Loc> Liberties { get; set; }

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