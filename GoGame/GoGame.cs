using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GoGame
{
    public partial class GoGame : Form
    {
        private int n;

        public GoGame()
        {
            InitializeComponent();
            this.n = 9;
        }

        private void paintSquares()
        {
            SplitterPanel gamePanel = mainSplitContainer.Panel1;
            int H = gamePanel.Height;
            int W = gamePanel.Width;

            bool isWider = W > H;

            int lesser = (isWider) ? H : W;
            int a = lesser / n;
            int numSquares = n - 1;

            float hpad = 0;
            float vpad = 0;

            if (isWider)
                hpad = Math.Abs(W - H) / 2f;
            else
                vpad = Math.Abs(W - H) / 2f;

            for (int i = 0; i < numSquares; ++i)
                for (int j = 0; j < numSquares; ++j)
                    using (Graphics g = gamePanel.CreateGraphics())
                    {
                        g.DrawRectangle(Pens.Black, hpad + (1 / 2f * a) + a * i, vpad + (1 / 2f * a) + a * j, a, a);
                    }
        }

        // iterate throught enumerable of stones, painting each one to the grid as it goes.
        private void paintStones()
        {

        }

        private void mainSplitContainer_Panel1_Paint(object sender, PaintEventArgs e)
        {
            this.paintSquares();
            this.paintStones();
        }

        private void mainSplitContainer_Panel1_Click(object sender, EventArgs e)
        {

        }
    }
}