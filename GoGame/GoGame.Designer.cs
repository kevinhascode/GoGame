namespace GoGame
{
    partial class GoGame
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.btnEditContinue = new System.Windows.Forms.Button();
            this.btnPass = new System.Windows.Forms.Button();
            this.btnNewGame = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            this.mainSplitContainer.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.mainSplitContainer_Panel1_Paint);
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.Controls.Add(this.btnEditContinue);
            this.mainSplitContainer.Panel2.Controls.Add(this.btnPass);
            this.mainSplitContainer.Panel2.Controls.Add(this.btnNewGame);
            this.mainSplitContainer.Size = new System.Drawing.Size(832, 466);
            this.mainSplitContainer.SplitterDistance = 608;
            this.mainSplitContainer.TabIndex = 0;
            // 
            // btnEditContinue
            // 
            this.btnEditContinue.Location = new System.Drawing.Point(65, 187);
            this.btnEditContinue.Name = "btnEditContinue";
            this.btnEditContinue.Size = new System.Drawing.Size(75, 42);
            this.btnEditContinue.TabIndex = 0;
            this.btnEditContinue.Text = "Edit / Continue";
            this.btnEditContinue.UseVisualStyleBackColor = true;
            // 
            // btnPass
            // 
            this.btnPass.Location = new System.Drawing.Point(65, 129);
            this.btnPass.Name = "btnPass";
            this.btnPass.Size = new System.Drawing.Size(75, 23);
            this.btnPass.TabIndex = 0;
            this.btnPass.Text = "Pass";
            this.btnPass.UseVisualStyleBackColor = true;
            // 
            // btnNewGame
            // 
            this.btnNewGame.Location = new System.Drawing.Point(65, 158);
            this.btnNewGame.Name = "btnNewGame";
            this.btnNewGame.Size = new System.Drawing.Size(75, 23);
            this.btnNewGame.TabIndex = 0;
            this.btnNewGame.Text = "New Game";
            this.btnNewGame.UseVisualStyleBackColor = true;
            // 
            // GoGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 466);
            this.Controls.Add(this.mainSplitContainer);
            this.Name = "GoGame";
            this.Text = "GoGame";
            this.Load += new System.EventHandler(this.GoGame_Load);
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.Button btnEditContinue;
        private System.Windows.Forms.Button btnPass;
        private System.Windows.Forms.Button btnNewGame;
    }
}