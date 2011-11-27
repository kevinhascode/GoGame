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

        private void GoGame_Load(object sender, EventArgs e)
        {
            this.paintSomeShit();
        }

        private void paintSomeShit()
        {
            SplitterPanel gamePanel = mainSplitContainer.Panel1;
            int H = gamePanel.Height;
            int W = gamePanel.Width;

            int totalLength = (W <= H) ? W : H;
            int a = totalLength / n;

            int D = Math.Abs(W - H);

            //for (int i = 0; i < n - 1; ++i)
            using (Graphics g = gamePanel.CreateGraphics())
            {
                using (Pen pen = new Pen(Color.Black, 2))
                {
                    using (Brush brush = new SolidBrush(gamePanel.BackColor))
                    {
                        //System.Drawing.Rectangle rect = new Rectangle(new Point(a, a), new Size(a, a));
                        g.DrawRectangle(pen, 100, 100, 100, 200);
                    }
                }
            }

            gamePanel.Invalidate();
        }

        private void mainSplitContainer_Panel1_Paint(object sender, PaintEventArgs e)
        {
            SplitterPanel gamePanel = mainSplitContainer.Panel1;
            int H = gamePanel.Height;
            int W = gamePanel.Width;

            int totalLength = (W <= H) ? W : H;
            int a = totalLength / n;

            int D = Math.Abs(W - H);

            //for (int i = 0; i < n - 1; ++i)
            using (Graphics g = gamePanel.CreateGraphics())
            {
                using (Pen pen = new Pen(Color.Black, 2))
                {
                    using (Brush brush = new SolidBrush(gamePanel.BackColor))
                    {
                        //System.Drawing.Rectangle rect = new Rectangle(new Point(a, a), new Size(a, a));
                        g.DrawRectangle(pen, 100, 100, 100, 200);
                    }
                }
            }

            gamePanel.Invalidate();
        }
    }
}