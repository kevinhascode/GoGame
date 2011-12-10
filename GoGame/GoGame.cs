using System.Collections.Generic;

namespace GoGame
{
    /// <summary>
    /// An instance of a game of Go.
    /// </summary>
    public class GoGame
    {
        private GameLogic Logic;
        public bool IsOver; // TODO: implement this for real... just exists for UnitTest right now.

        private short boardSize;
        public short BoardSize
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
        public short Handicap
        {
            get { return this.handicap; }
            set
            {
                this.handicap = value;
                Properties.Settings.Default.Handicap = value;
                Properties.Settings.Default.Save();
            }
        }

        public readonly List<Chain> whiteChains = new List<Chain>();
        public readonly List<Chain> blackChains = new List<Chain>();

        public List<Loc> LocsPlayed
        {
            get
            {
                List<Loc> played = new List<Loc>();

                foreach (Chain chain in whiteChains)
                    foreach (Stone stone in chain.Stones)
                        played.Add(stone.Loc);

                foreach (Chain chain in blackChains)
                    foreach (Stone stone in chain.Stones)
                        played.Add(stone.Loc);

                return played;
            }
        }

        public int PrisonersTakenByWhite { get; private set; }
        public int PrisonersTakenByBlack { get; private set; }

        public Loc PossibleKoLoc { get; set; }

        public GoGame()
        {
            this.boardSize = Properties.Settings.Default.BoardSize;
            this.handicap = Properties.Settings.Default.Handicap;
            this.PrisonersTakenByWhite = 0;
            this.PrisonersTakenByBlack = 0;

            this.Logic = new GameLogic(this);
        }

        public void Reset()
        {
            this.PrisonersTakenByWhite = 0;
            this.PrisonersTakenByBlack = 0;
            this.whiteChains.Clear();
            this.blackChains.Clear();

            this.Logic.Reset(this);
        }

        public RequestResponse ProposedPlay(Loc loc)
        {
            if (this.Logic.IsLegal(loc))
                return this.Logic.PlaceStone(loc);
            else
                return new RequestResponse(ReasonEnum.Conflict);
        }
    }
}