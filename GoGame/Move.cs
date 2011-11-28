using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoGame
{
    // TODO: Where does this really belong? 
    internal class Move
    {
        internal Stone StonePlaced { get; set; }
        internal List<Chain> ChainsKilled { get; set; }
    }
}