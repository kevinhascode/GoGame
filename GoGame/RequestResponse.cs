using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoGame
{
    internal class RequestResponse
    {
        internal string Reason { get; set; }
        internal Stone Stone { get; set; }

        public RequestResponse(Stone stone)
        {
            this.Stone = stone;
        }

        public RequestResponse(string reason)
        {
            this.Reason = reason;
        }
    }
}