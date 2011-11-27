using System;
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
    public partial class GoGame : Form
    {
        private int n;
        private readonly List<Stone> stones = new List<Stone>();
        private readonly SplitterPanel gamePanel;

        public GoGame()
        {
            InitializeComponent();

            this.n = 9;
            this.gamePanel = this.mainSplitContainer.Panel1;
        }

        private void paintSquares()
        {
            int hpad, vpad, a;
            this.calcGridValues(out hpad, out vpad, out a);

            int numSquares = n - 1;

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

            foreach (Stone stone in this.stones)
                using (SolidBrush brush = new SolidBrush(Color.Black))
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
            a = lesser / n;

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

            using (Graphics g = gamePanel.CreateGraphics())
            {
                g.DrawRectangle(Pens.Red, X - 5, Y - 5, 10, 10);
            }

            int sqX = (X - hpad) / a;
            int sqY = (Y - vpad) / a;

            // TODO: need to fix guesses that go off the grid to the right.

            //MessageBox.Show(String.Format("clickLoc:: X: {0} Y: {1}\r\nI think this is square:({2},{3})", X, Y, sqX, sqY));

            this.stones.Add(new Stone(new Loc(sqX, sqY), true));
        }
    }
}