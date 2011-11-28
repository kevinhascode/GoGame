using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoGame
{
    internal class RequestResponse
    {
        internal string Reason { get; set; }
        internal Move Move { get; set; }

        public RequestResponse(Move move)
        {
            this.Move = move;
        }

        public RequestResponse(string reason)
        {
            this.Reason = reason;
        }
    }
}