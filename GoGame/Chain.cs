using System.Collections.Generic;
using System.Linq;

namespace GoGame
{
    public class Chain
    {
        public readonly List<Stone> Stones;
        public List<Loc> Liberties { get; set; }

        public bool IsWhite
        {
            get { return this.Stones.First().IsWhite; }
        }

        public Chain()
        {
            this.Stones = new List<Stone>();
            this.Liberties = new List<Loc>();
        }

        public Chain(Stone stone, List<Loc> liberties)
            : this()
        {
            this.Stones.Add(stone);
            this.Liberties = liberties;
        }

        // Convenience method for merging multiple chains
        public static Chain Merge(List<Chain> chains)
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