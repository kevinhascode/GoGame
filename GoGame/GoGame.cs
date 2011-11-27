using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        private int prisonersTakenByWhite { get; set; }
        private int prisonersTakenByBlack { get; set; }

        public GoGame()
        {
            this.boardSize = Properties.Settings.Default.BoardSize;
            this.handicap = Properties.Settings.Default.Handicap;
            this.prisonersTakenByWhite = 0;
            this.prisonersTakenByBlack = 0;
        }

        internal void Reset()
        {
            this.prisonersTakenByBlack = 0;
            this.prisonersTakenByWhite = 0;
        }
    }
}