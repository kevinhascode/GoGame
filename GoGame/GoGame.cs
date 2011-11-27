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
            this.n = 3;
        }

        private void paintSquares()
        {
            SplitterPanel gamePanel = mainSplitContainer.Panel1;
            int H = gamePanel.Height;
            int W = gamePanel.Width;

            bool isWider = W > H;

            int totalLength = (isWider) ? W : H;
            int a = totalLength / n;
            int numSquares = n - 1;
            int D = Math.Abs(W - H);

            for (int i = 1; i <= numSquares; ++i)
                for (int j = 1; j <= numSquares; ++j)
                    using (Graphics g = gamePanel.CreateGraphics())
                    {
                        if (isWider)
                            g.DrawRectangle(Pens.Black, a * i + D, a * j, a, a);
                        else
                            g.DrawRectangle(Pens.Black, a * i, a * j + D, a, a);

                    }
        }

        private void mainSplitContainer_Panel1_Paint(object sender, PaintEventArgs e)
        {
            this.paintSquares();
        }
    }
}