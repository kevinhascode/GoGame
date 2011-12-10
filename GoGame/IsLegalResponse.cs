using System.Collections.Generic;

namespace GoGame
{
    public class IsLegalResponse
    {
        public ReasonEnum Reason { get; set; }
        public List<Chain> Killed { get; set; }
        public List<Chain> MergeResult { get; set; }

        public IsLegalResponse(List<Chain> killed, List<Chain> mergeResult)
        {
            this.Killed = killed;
            this.MergeResult = mergeResult;
        }

        public IsLegalResponse(ReasonEnum reason)
        {
            this.Reason = reason;
        }
    }
}