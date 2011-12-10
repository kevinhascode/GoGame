namespace GoGame
{
    internal class RequestResponse
    {
        internal ReasonEnum Reason { get; set; }
        internal Move Move { get; set; }

        public RequestResponse(Move move)
        {
            this.Move = move;
        }

        public RequestResponse(ReasonEnum reason)
        {
            this.Reason = reason;
        }
    }
}