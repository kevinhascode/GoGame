using System.Collections.Generic;

namespace GoGame
{
    /// <summary>
    /// An instance of a game of Go.
    /// </summary>
    internal class GoGame
    {
        private short boardSize;
        internal short BoardSize
        {
            get
            { return this.boardSize; }
            set
            {
                this.boardSize = value;
                Properties.Settings.Default.BoardSize = value;
                Properties.Settings.Default.Save();
            }
        }

        private short handicap;
        internal short Handicap
        {
            get { return this.handicap; }
            set
            {
                this.handicap = value;
                Properties.Settings.Default.Handicap = value;
                Properties.Settings.Default.Save();
            }
        }

        internal readonly List<Chain> whiteChains = new List<Chain>();
        internal readonly List<Chain> blackChains = new List<Chain>();

        private GameLogic Logic;

        internal bool IsOver;

        internal int PrisonersTakenByWhite { get; private set; }
        internal int PrisonersTakenByBlack { get; private set; }

        internal Loc PossibleKoLoc { get; private set; }

        public GoGame()
        {
            this.boardSize = Properties.Settings.Default.BoardSize;
            this.handicap = Properties.Settings.Default.Handicap;
            this.PrisonersTakenByWhite = 0;
            this.PrisonersTakenByBlack = 0;

            this.Logic = new GameLogic(this);
        }

        internal void Reset()
        {
            this.PrisonersTakenByWhite = 0;
            this.PrisonersTakenByBlack = 0;
            this.whiteChains.Clear();
            this.blackChains.Clear();

            GameLogic.Reset(this);
        }

        internal RequestResponse ProposedPlay(Loc loc)
        {
            //TODO: If there is a kill, and it's 1 chain, w/ one stone, possibleKoLoc = loc

            if (this.Logic.IsLegal(loc))
                return this.Logic.PlaceStone(loc);
            else
                return new RequestResponse(ReasonEnum.Conflict);
        }
    }
}