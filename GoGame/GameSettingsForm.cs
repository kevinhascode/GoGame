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
        MainForm mainForm;
        string goodSize, goodHandicap;

        //Game Modes -- Local Game/Network Game/ Play through Saved Game?
        static readonly string[ ] SIZERANGE = new string[ ] { "", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19" };
        static readonly string[ ] SMALLRANGE = new string[ ] { "0", "2", "3", "4", "5" };
        static readonly string[ ] LARGERANGE = new string[ ] { "0", "2", "3", "4", "5", "6", "7", "8", "9" };

        internal GameSettingsForm(MainForm mainForm)
        {
            this.mainForm = mainForm;

            InitializeComponent();

            this.goodSize = this.mainForm.Game.BoardSize.ToString();
            this.cmbBoardSize.Text = this.mainForm.Game.BoardSize.ToString();
            this.goodHandicap = this.mainForm.Game.Handicap.ToString();
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
            if( !SIZERANGE.Contains( this.cmbBoardSize.Text ) )
            {
                MessageBox.Show( "Pick one from the list.", "Illegal value" );
                this.cmbBoardSize.Text = this.goodSize;
                this.cmbBoardSize.Focus();
                return;
            }

            if( this.cmbBoardSize.Text == "" )
                return;

            short size = short.Parse( this.cmbBoardSize.Text );

            if( size >= 7 )
            {
                this.cmbHandicap.Enabled = true;
                this.cmbHandicap.Items.Clear();
                this.cmbHandicap.Items.AddRange( LARGERANGE );
                if( LARGERANGE.Contains( this.goodHandicap ) )
                    this.cmbHandicap.Text = this.goodHandicap;
                else
                    this.cmbHandicap.Text = "0";
            }
            else if( size <= 2 )
            {
                this.cmbHandicap.Text = "0";
                this.cmbHandicap.Enabled = false;
            }
            else
            {
                this.cmbHandicap.Enabled = true;
                this.cmbHandicap.Items.Clear();
                this.cmbHandicap.Items.AddRange( SMALLRANGE );
                if( SMALLRANGE.Contains( this.goodHandicap ) )
                    this.cmbHandicap.Text = this.goodHandicap;
                else
                    this.cmbHandicap.Text = SMALLRANGE.Last();
            }

            this.goodSize = this.cmbBoardSize.Text;
        }

        private void cmbBoardSize_Leave(object sender, EventArgs e)
        {   // text must be contained in SIZERANGE, which is enforced by TextChanged, just that we don't want to allow a 1x1
            if( this.cmbBoardSize.Text == "1" || this.cmbBoardSize.Text == "" )
            {
                MessageBox.Show( "Pick one from the list.", "Illegal value" );
                this.cmbBoardSize.Focus();
            }
        }

        private void cmbHandicap_TextChanged(object sender, EventArgs e)
        {
            short size = short.Parse( this.cmbBoardSize.Text );

            if( size >= 7 )
            {
                if( !GameSettingsForm.LARGERANGE.Contains( this.cmbHandicap.Text ) )
                {
                    MessageBox.Show( "Pick one from the list.", "Illegal value" );
                    this.cmbHandicap.Text = this.goodHandicap;
                    this.cmbHandicap.Focus();
                    return;
                }
            }
            else
                if( !GameSettingsForm.SMALLRANGE.Contains( this.cmbHandicap.Text ) )
                {
                    MessageBox.Show( "Pick one from the list.", "Illegal value" );
                    this.cmbHandicap.Text = this.goodHandicap;
                    this.cmbHandicap.Focus();
                    return;
                }

            this.goodHandicap = this.cmbHandicap.Text;
        }
    }
}