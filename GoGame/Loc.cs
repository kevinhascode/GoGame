namespace GoGame
{
    // TODO: change X and Y to shorts

    /// <summary>
    /// Used to keep track of where a stone is placed.
    /// </summary>
    internal class Loc
    {
        internal int X { get; set; }
        internal int Y { get; set; }

        public Loc(int x, int y)
        {
            X = x;
            Y = y;
        }

        internal bool Equals(Loc loc)
        {
            return (loc.X == this.X) && (loc.Y == this.Y);
        }
    }
}