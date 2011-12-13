using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace GoGame
{
    public partial class MainForm : Form
    {
        // public so it's reachable from GameSettingsForm
        public GoGame Game;
        // Field will only be used for painting purposes. Yes, that means maintaining two separate lists of plays.
        // One of stones for painting and one of chains for logical purposes.
        private readonly List<Stone> stonesForPainting = new List<Stone>();
        private readonly SplitterPanel gamePanel;

        public MainForm()
        {
            InitializeComponent();

            this.Game = new GoGame();
            this.gamePanel = this.mainSplitContainer.Panel1;
        }

        // Resets Form's part of the game
        public void NewGame()
        {
            this.gamePanel.Invalidate();

            //clear object to be painted
            this.stonesForPainting.Clear();

            this.Game = new GoGame();

            //TODO: remove Hoshi
            //TODO: add Hoshi

            //repaint
            mainSplitContainer_Panel1_Paint(this, null);
        }

        #region Painting code...

        private void paintSquares()
        {
            int hpad, vpad, a;
            this.calcGridValues(out hpad, out vpad, out a);

            int numSquares = this.Game.BoardSize - 1;

            for (int i = 0; i < numSquares; ++i)
                for (int j = 0; j < numSquares; ++j)
                    using (Graphics g = gamePanel.CreateGraphics())
                        g.DrawRectangle(Pens.Black, hpad + (1 / 2f * a) + a * i, vpad + (1 / 2f * a) + a * j, a, a);
        }

        private void paintLines()
        {
            int hpad, vpad, a;
            this.calcGridValues(out hpad, out vpad, out a);

            int boardWidth = this.gamePanel.Width - (2 * hpad);
            int bw = boardWidth;
            int n = this.Game.BoardSize;
            int lineLength = bw * (n - 1) / n;
            int sqsz = bw / n;

            for (int lineNum = 0; lineNum < n; ++lineNum)
            {
                using (Graphics g = gamePanel.CreateGraphics())
                {
                    // horizontal lines
                    g.DrawLine(Pens.Black, (hpad + sqsz / 2), (vpad + sqsz / 2) + (lineNum * sqsz),
                        (hpad + sqsz / 2 + lineLength), (vpad + sqsz / 2) + (lineNum * sqsz));
                    // vertical lines
                    g.DrawLine(Pens.Black, (hpad + sqsz / 2) + (lineNum * sqsz), vpad + sqsz / 2,
                        (hpad + sqsz / 2) + (lineNum * sqsz), vpad + sqsz / 2 + lineLength);
                }
            }
        }

        // iterate through enumerable of stones, painting each one to the grid as it goes.
        private void paintStones()
        {
            int hpad, vpad, a;
            this.calcGridValues(out hpad, out vpad, out a);

            foreach (Stone stone in this.stonesForPainting)
                using (SolidBrush brush = new SolidBrush((stone.IsWhite) ? Color.White : Color.Black))
                {
                    using (Graphics g = gamePanel.CreateGraphics())
                    {
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                        g.FillEllipse(brush, hpad + (stone.Loc.X * a), vpad + (stone.Loc.Y * a), a, a);
                    }
                }
        }

        private void mainSplitContainer_Panel1_Paint(object sender, PaintEventArgs e)
        {
            //Stopwatch sw = Stopwatch.StartNew();
            this.paintSquares();
            //this.paintLines();
            //sw.Stop();
            //Console.WriteLine(sw.Elapsed);

            this.addHoshi();
            this.paintStones();
        }

        private void calcGridValues(out int hpad, out int vpad, out int squareSize)
        {
            int H = this.gamePanel.Height;
            int W = this.gamePanel.Width;

            bool isWider = W > H;

            int lesser = (isWider) ? H : W;
            squareSize = lesser / this.Game.BoardSize;

            if (isWider)
            {
                hpad = Math.Abs(W - H) / 2;
                vpad = 0;
            }
            else
            {
                hpad = 0;
                vpad = Math.Abs(W - H) / 2;
            }
        }

        /* Logic for where to paint Hoshi
         * Size:    Hoshi:
         * 2-4      NONE
         * 5        1
         * 6        NONE
         * 7        1
         * 8-11     4 OR 5
         * 12-19    4 OR 9
         */
        private void addHoshi()
        {
            int hpad, vpad, sqsz;
            this.calcGridValues(out hpad, out vpad, out sqsz);
            int boardWidth = this.gamePanel.Width - (2 * hpad);
            int bw = boardWidth;
            int n = this.Game.BoardSize;

            if (this.Game.BoardSize >= 12)
            {
                int right = (int)(hpad + sqsz * (n - 3.5)) - 2;
                int left = (int)(hpad + sqsz * 3.5) - 2;
                int top = (int)(vpad + sqsz * 3.5) - 2;
                int bottom = (int)(vpad + sqsz * (n - 3.5)) - 2;

                this.paintHoshi(left, top);
                this.paintHoshi(right, top);
                this.paintHoshi(left, bottom);
                this.paintHoshi(right, bottom);

                if (this.Game.BoardSize % 2 != 0)
                {   // if odd
                    int middleH = (int)(hpad + 1 / 2f * bw) - 2;
                    int middleV = (int)(vpad + 1 / 2f * bw) - 2;

                    this.paintHoshi(middleH, middleV);

                    this.paintHoshi(middleH, top);
                    this.paintHoshi(right, middleV);
                    this.paintHoshi(middleH, bottom);
                    this.paintHoshi(left, middleV);
                }

                return;
            }
            if (this.Game.BoardSize >= 8)
            {
                int b = this.Game.BoardSize - 3;
                this.paintHoshi((int)(hpad + sqsz * 2.5) - 2, (int)(vpad + sqsz * 2.5) - 2);
                this.paintHoshi((int)(hpad + sqsz * (n - 2.5)) - 2, (int)(vpad + sqsz * 2.5) - 2);
                this.paintHoshi((int)(hpad + sqsz * 2.5) - 2, (int)(vpad + sqsz * (n - 2.5)) - 2);
                this.paintHoshi((int)(hpad + sqsz * (n - 2.5)) - 2, (int)(vpad + sqsz * (n - 2.5)) - 2);

                if (this.Game.BoardSize % 2 != 0)
                {   // if odd
                    // Paint at center
                    this.paintHoshi((int)(hpad + 1 / 2f * bw) - 2, (int)(vpad + 1 / 2f * bw) - 2);
                }

                return;
            }
            if (this.Game.BoardSize == 5 || this.Game.BoardSize == 7)
            {
                this.paintHoshi((int)(hpad + 1 / 2f * bw) - 2, (int)(vpad + 1 / 2f * bw) - 2);

                return;
            }
        }

        private void paintHoshi(int x, int y)
        {
            using (Graphics g = gamePanel.CreateGraphics())
                g.FillRectangle(Brushes.Black, x, y, 5, 5);
        }

        private void paintHoshi(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.Black, this.Width / 2 - 2, this.Height / 2 - 2, 5, 5);
        }

        #endregion

        private void mainSplitContainer_Panel1_Click(object sender, EventArgs e)
        {
            int hpad, vpad, a;
            this.calcGridValues(out hpad, out vpad, out a);

            int X = ((MouseEventArgs)e).X;
            int Y = ((MouseEventArgs)e).Y;
            int sqX = (X - hpad) / a;
            int sqY = (Y - vpad) / a;

            // takes care of clicks to the right of (or below) the grid
            if (sqX >= this.Game.BoardSize || sqY >= this.Game.BoardSize)
                return;
            // takes care of clicks to the left of (or above) the grid
            else if ((X - hpad) < 0 || (Y - vpad) < 0)
                return;
            else
            {
                RequestResponse response = this.Game.ProposedPlay(new Loc(sqX, sqY));

                if (response.Reason == ReasonEnum.Fine)
                {
                    // Add the stone that was played.
                    this.stonesForPainting.Add(response.Move.StonePlaced);

                    // Remove all of the stones that were killed.
                    foreach (Chain chainKilled in response.Move.ChainsKilled)
                        foreach (Stone stoneKilled in chainKilled.Stones)
                            this.stonesForPainting.Remove(this.stonesForPainting.Find(paintedStone => paintedStone.Loc.Equals(stoneKilled.Loc)));
                }
                else
                    switch (response.Reason)
                    {
                        case ReasonEnum.Conflict:
                            MessageBox.Show("Illegal: Can't play on top of another stone.", "I don't think so!");
                            break;
                        case ReasonEnum.Ko:
                            MessageBox.Show("Illegal: Ko move.", "I don't think so!");
                            break;
                        case ReasonEnum.Suicide:
                            MessageBox.Show("Illegal: Can't commit suicide.", "I don't think so!");
                            break;
                    }
            }

            this.gamePanel.Invalidate();
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            this.NewGame();
        }

        #region Tool Strip Items

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new GameSettingsForm(this).ShowDialog();
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.NewGame();
        }

        #endregion

        private void btnPass_Click(object sender, EventArgs e)
        {
            this.Game.ProposedPlay(new Loc(this.Game.BoardSize, this.Game.BoardSize));
        }

        private void btnEditContinue_Click(object sender, EventArgs e)
        {
            // TODO: Implement this.
            MessageBox.Show("Implement Me!!");
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string log = this.Game.GenerateLog();
        }
    }
}