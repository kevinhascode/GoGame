using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoGame
{
    internal class Move
    {
        internal Stone StonePlaced { get; set; }
        internal List<Chain> ChainsKilled { get; set; }
    }
}