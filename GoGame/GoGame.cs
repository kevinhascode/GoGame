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

        public List<Stone> StonesPlayed
        {
            get
            {
                List<Stone> played = new List<Stone>();

                foreach (Chain chain in whiteChains)
                    played.AddRange(chain.Stones);

                foreach (Chain chain in blackChains)
                    played.AddRange(chain.Stones);

                return played;
            }
        }

        public List<Loc> LocsPlayed
        {
            get
            {
                List<Loc> played = new List<Loc>();

                foreach (Stone stone in this.StonesPlayed)
                    played.Add(stone.Loc);

                return played;
            }
        }

        public int PrisonersTakenByWhite { get; set; }
        public int PrisonersTakenByBlack { get; set; }

        public Loc PossibleKoLoc = new Loc(-1, -1);
        public bool LastWasPass;

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

        // TODO: implement PASSing
        public RequestResponse ProposedPlay(Loc loc)
        {
            // if it's ==, then it's a PASS
            if (loc.X == this.boardSize)
            {
                if (this.LastWasPass)
                {
                    this.IsOver = true;
                    // TODO: game-ending logic goes here.
                }

                this.LastWasPass = true;
                this.Logic.ChangeTurn();
                return new RequestResponse(ReasonEnum.Pass);
            }
            else
                this.LastWasPass = false;

            IsLegalResponse isLegalResponse = this.Logic.IsLegal(loc);

            if (isLegalResponse.Reason == ReasonEnum.Fine)
                return this.Logic.PlaceStone(loc, isLegalResponse);
            else
                return new RequestResponse(isLegalResponse.Reason);
        }
    }
}