namespace GoGame
{
    // TODO: change X and Y to shorts

    /// <summary>
    /// Used to keep track of where a stone is placed.
    /// </summary>
    public class Loc
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Loc(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Loc)) return false;
            return Equals((Loc)obj);
        }

        public bool Equals(Loc other)
        {
            if (ReferenceEquals(this, other)) return true;
            return (other.X == this.X) && (other.Y == this.Y);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 100) + Y;
            }
        }
    }
}