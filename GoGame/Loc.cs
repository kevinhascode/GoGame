using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoGame
{
    internal class Loc
    {
        internal int X { get; set; }
        internal int Y { get; set; }

        public Loc(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}