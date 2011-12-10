namespace GoGame
{
    /// <summary>
    /// Represents a stone on the board, nothing more.
    /// </summary>
    public class Stone
    {
        public Loc Loc { get; set; }
        public bool IsWhite { get; set; }

        public Stone(Loc loc, bool isWhite)
        {
            Loc = loc;
            IsWhite = isWhite;
        }
    }
}