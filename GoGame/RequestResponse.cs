namespace GoGame
{
    public class RequestResponse
    {
        public ReasonEnum Reason { get; set; }
        public Move Move { get; set; }

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