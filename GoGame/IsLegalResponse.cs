using System.Collections.Generic;

namespace GoGame
{
    public class IsLegalResponse
    {
        public ReasonEnum Reason { get; set; }
        public List<Chain> Killed;
        public Chain MergeResultant;
        public List<Chain> AbsorbedInMerge;

        public IsLegalResponse(ReasonEnum reason = ReasonEnum.Fine)
        {
            this.Reason = reason;
            this.Killed = new List<Chain>();
            this.MergeResultant = new Chain();
            this.AbsorbedInMerge = new List<Chain>();
        }
    }
}