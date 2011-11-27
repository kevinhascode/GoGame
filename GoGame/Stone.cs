using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoGame
{
    internal class Stone
    {
        internal Loc Loc { get; set; }
        internal bool IsWhite { get; set; }

        public Stone(Loc loc , bool isWhite)
        {
            Loc = loc;
            IsWhite = isWhite;
        }
    }
}