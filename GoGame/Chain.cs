using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoGame
{
    internal class Chain
    {
        internal List<Stone> Stones { get; set; }
        internal List<Loc> Liberties { get; set; }
    }
}