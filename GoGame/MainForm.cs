﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace GoGame
{
    public partial class MainForm : Form
    {
        internal GoGame Game;
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
        internal void NewGame()
        {
            this.gamePanel.Invalidate();

            //clear object to be painted
            this.stonesForPainting.Clear();

            this.Game = new GoGame();

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
                    {
                        g.DrawRectangle(Pens.Black, hpad + (1 / 2f * a) + a * i, vpad + (1 / 2f * a) + a * j, a, a);
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
            this.paintSquares();
            this.paintStones();
        }

        private void calcGridValues(out int hpad, out int vpad, out int a)
        {
            int H = this.gamePanel.Height;
            int W = this.gamePanel.Width;

            bool isWider = W > H;

            int lesser = (isWider) ? H : W;
            a = lesser / this.Game.BoardSize;

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

        private void mainSplitContainer_Panel1_Click(object sender, EventArgs e)
        {
            int hpad, vpad, a;
            this.calcGridValues(out hpad, out vpad, out a);

            int X = ((MouseEventArgs)e).X;
            int Y = ((MouseEventArgs)e).Y;
            int sqX = (X - hpad) / a;
            int sqY = (Y - vpad) / a;

            #region DEBUG code
            
            //using (Graphics g = gamePanel.CreateGraphics())
            //{
            //    g.DrawRectangle(Pens.Red, X - 5, Y - 5, 10, 10);
            //}
            //MessageBox.Show(String.Format("clickLoc:: X: {0} Y: {1}\r\nI think this is square:({2},{3})", X, Y, sqX, sqY));
            
            #endregion

            // takes care of clicks to the right of (or below) the grid
            if (sqX >= this.Game.BoardSize || sqY >= this.Game.BoardSize)
                return;
            // takes care of clicks to the left of (or above) the grid
            else if ((X - hpad) < 0 || (Y - vpad) < 0)
                return;
            else
            {
                RequestResponse response = this.Game.ProposedPlay(new Loc(sqX, sqY));

                if (response.Stone != null)
                    this.stonesForPainting.Add(response.Stone);
                else
                    MessageBox.Show(response.Reason);
            }

            this.gamePanel.Invalidate();
        }

        #endregion

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            // TODO: implement
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
    }
}