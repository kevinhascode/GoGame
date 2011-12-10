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
    }
}