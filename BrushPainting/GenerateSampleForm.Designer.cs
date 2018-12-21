namespace BrushPainting
{
    partial class GenerateSampleForm
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
            this.CurrentPictureBox = new System.Windows.Forms.PictureBox();
            this.GeneratingProgressBar = new System.Windows.Forms.ProgressBar();
            this.CurrentPictureNameLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // CurrentPictureBox
            // 
            this.CurrentPictureBox.Location = new System.Drawing.Point(24, 21);
            this.CurrentPictureBox.Name = "CurrentPictureBox";
            this.CurrentPictureBox.Size = new System.Drawing.Size(360, 276);
            this.CurrentPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.CurrentPictureBox.TabIndex = 0;
            this.CurrentPictureBox.TabStop = false;
            // 
            // GeneratingProgressBar
            // 
            this.GeneratingProgressBar.Location = new System.Drawing.Point(24, 337);
            this.GeneratingProgressBar.Name = "GeneratingProgressBar";
            this.GeneratingProgressBar.Size = new System.Drawing.Size(360, 23);
            this.GeneratingProgressBar.TabIndex = 1;
            // 
            // CurrentPictureNameLabel
            // 
            this.CurrentPictureNameLabel.AutoSize = true;
            this.CurrentPictureNameLabel.Location = new System.Drawing.Point(24, 304);
            this.CurrentPictureNameLabel.Name = "CurrentPictureNameLabel";
            this.CurrentPictureNameLabel.Size = new System.Drawing.Size(123, 15);
            this.CurrentPictureNameLabel.TabIndex = 2;
            this.CurrentPictureNameLabel.Text = "CurrentPictureName";
            // 
            // GenerateSampleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 379);
            this.Controls.Add(this.CurrentPictureNameLabel);
            this.Controls.Add(this.GeneratingProgressBar);
            this.Controls.Add(this.CurrentPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "GenerateSampleForm";
            this.Text = "GenerateSampleForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GenerateSampleForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.CurrentPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox CurrentPictureBox;
        private System.Windows.Forms.ProgressBar GeneratingProgressBar;
        private System.Windows.Forms.Label CurrentPictureNameLabel;
    }
}