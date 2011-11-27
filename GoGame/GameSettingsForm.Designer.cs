namespace GoGame
{
    partial class GameSettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cmbHandicap = new System.Windows.Forms.ComboBox();
            this.lblSize = new System.Windows.Forms.Label();
            this.lblHandicap = new System.Windows.Forms.Label();
            this.cmbBoardSize = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point( 20, 87 );
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size( 75, 23 );
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler( this.OK_Click );
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point( 119, 87 );
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size( 75, 23 );
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler( this.Cancel_Click );
            // 
            // cmbHandicap
            // 
            this.cmbHandicap.DropDownWidth = 99;
            this.cmbHandicap.ItemHeight = 13;
            this.cmbHandicap.Items.AddRange( new object[ ] {
            "0"} );
            this.cmbHandicap.Location = new System.Drawing.Point( 79, 50 );
            this.cmbHandicap.Name = "cmbHandicap";
            this.cmbHandicap.Size = new System.Drawing.Size( 99, 21 );
            this.cmbHandicap.TabIndex = 1;
            this.cmbHandicap.Text = "0";
            this.cmbHandicap.TextChanged += new System.EventHandler( this.cmbHandicap_TextChanged );
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Location = new System.Drawing.Point( 12, 26 );
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size( 61, 13 );
            this.lblSize.TabIndex = 4;
            this.lblSize.Text = "Board Size:";
            // 
            // lblHandicap
            // 
            this.lblHandicap.AutoSize = true;
            this.lblHandicap.Location = new System.Drawing.Point( 17, 53 );
            this.lblHandicap.Name = "lblHandicap";
            this.lblHandicap.Size = new System.Drawing.Size( 56, 13 );
            this.lblHandicap.TabIndex = 4;
            this.lblHandicap.Text = "Handicap:";
            // 
            // cmbBoardSize
            // 
            this.cmbBoardSize.DropDownWidth = 99;
            this.cmbBoardSize.FormattingEnabled = true;
            this.cmbBoardSize.ItemHeight = 13;
            this.cmbBoardSize.Items.AddRange( new object[ ] {
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19"} );
            this.cmbBoardSize.Location = new System.Drawing.Point( 79, 23 );
            this.cmbBoardSize.Name = "cmbBoardSize";
            this.cmbBoardSize.Size = new System.Drawing.Size( 99, 21 );
            this.cmbBoardSize.TabIndex = 0;
            this.cmbBoardSize.Leave += new System.EventHandler( this.cmbBoardSize_Leave );
            this.cmbBoardSize.TextChanged += new System.EventHandler( this.cmbBoardSize_TextChanged );
            // 
            // GameSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 212, 138 );
            this.Controls.Add( this.cmbBoardSize );
            this.Controls.Add( this.lblHandicap );
            this.Controls.Add( this.lblSize );
            this.Controls.Add( this.cmbHandicap );
            this.Controls.Add( this.btnCancel );
            this.Controls.Add( this.btnOK );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "GameSettingsForm";
            this.Text = "Game Settings";
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cmbHandicap;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Label lblHandicap;
        private System.Windows.Forms.ComboBox cmbBoardSize;
    }
}