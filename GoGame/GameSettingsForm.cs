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
    internal partial class GameSettingsForm : Form
    {
        private MainForm mainForm;
        private string lastGoodSize, lastGoodHandicap;

        //Game Modes -- Local Game/Network Game/ Play through Saved Game?
        private static readonly string[] boardSizeRange = new string[] { "", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19" };
        private static readonly string[] smallHandicapRange = new string[] { "0", "2", "3", "4", "5" };
        private static readonly string[] largeHandicapRange = new string[] { "0", "2", "3", "4", "5", "6", "7", "8", "9" };

        internal GameSettingsForm(MainForm mainForm)
        {
            this.mainForm = mainForm;

            InitializeComponent();

            this.lastGoodSize = this.mainForm.Game.BoardSize.ToString();
            this.cmbBoardSize.Text = this.mainForm.Game.BoardSize.ToString();
            this.lastGoodHandicap = this.mainForm.Game.Handicap.ToString();
            this.cmbHandicap.Text = this.mainForm.Game.Handicap.ToString();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            this.mainForm.Game.Handicap = short.Parse(this.cmbHandicap.Text);
            this.mainForm.Game.BoardSize = short.Parse(this.cmbBoardSize.Text);
            this.mainForm.NewGame();

            this.Dispose();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cmbBoardSize_TextChanged(object sender, EventArgs e)
        {
            if (!boardSizeRange.Contains(this.cmbBoardSize.Text))
            {
                MessageBox.Show("Pick one from the list.", "Illegal value");
                this.cmbBoardSize.Text = this.lastGoodSize;
                this.cmbBoardSize.Focus();
                return;
            }

            if (this.cmbBoardSize.Text == "")
                return;

            short size = short.Parse(this.cmbBoardSize.Text);

            // If the board is big enough, the player can choose from the large handicap range.
            if (size >= 7)
            {
                this.cmbHandicap.Enabled = true;
                this.cmbHandicap.Items.Clear();
                this.cmbHandicap.Items.AddRange(largeHandicapRange);
                if (largeHandicapRange.Contains(this.lastGoodHandicap))
                    this.cmbHandicap.Text = this.lastGoodHandicap;
                else
                    this.cmbHandicap.Text = largeHandicapRange.Last();
            }
            else if (size <= 2)
            {
                this.cmbHandicap.Text = "0";
                this.cmbHandicap.Enabled = false;
            }
            else
            {
                this.cmbHandicap.Enabled = true;
                this.cmbHandicap.Items.Clear();
                this.cmbHandicap.Items.AddRange(smallHandicapRange);
                if (smallHandicapRange.Contains(this.lastGoodHandicap))
                    this.cmbHandicap.Text = this.lastGoodHandicap;
                else
                    this.cmbHandicap.Text = smallHandicapRange.Last();
            }

            this.lastGoodSize = this.cmbBoardSize.Text;
        }

        // text must be contained in SIZERANGE, which is enforced by TextChanged
        // but TextChanged isn't fired off when all the text is deleted
        private void cmbBoardSize_Leave(object sender, EventArgs e)
        {
            if (this.cmbBoardSize.Text == "")
            {
                MessageBox.Show("Pick one from the list.", "Illegal value");
                this.cmbBoardSize.Focus();
            }
        }

        private void cmbHandicap_TextChanged(object sender, EventArgs e)
        {
            short size = short.Parse(this.cmbBoardSize.Text);

            if (size >= 7)
            {
                if (!GameSettingsForm.largeHandicapRange.Contains(this.cmbHandicap.Text))
                {
                    MessageBox.Show("Pick one from the list.", "Illegal value");
                    this.cmbHandicap.Text = this.lastGoodHandicap;
                    this.cmbHandicap.Focus();
                    return;
                }
            }
            else if (!GameSettingsForm.smallHandicapRange.Contains(this.cmbHandicap.Text))
            {
                MessageBox.Show("Pick one from the list.", "Illegal value");
                this.cmbHandicap.Text = this.lastGoodHandicap;
                this.cmbHandicap.Focus();
                return;
            }

            this.lastGoodHandicap = this.cmbHandicap.Text;
        }
    }
}