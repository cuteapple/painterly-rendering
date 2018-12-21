namespace BrushPainting
{
    partial class TopForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label StyleLabel;
            System.Windows.Forms.Label BackgroundColorLable;
            this.SourcePictureBox = new System.Windows.Forms.PictureBox();
            this.ResultPictureBox = new System.Windows.Forms.PictureBox();
            this.ResultMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ResultSaveButton = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadImageButton = new System.Windows.Forms.Button();
            this.StyleComboBox = new System.Windows.Forms.ComboBox();
            this.StyleErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.StartProcessButton = new System.Windows.Forms.Button();
            this.RefImageErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.FullPanel = new System.Windows.Forms.Panel();
            this.GenerateSampleButton = new System.Windows.Forms.Button();
            this.SelectBkgCokorButton = new System.Windows.Forms.Button();
            this.LoadSampleButton = new System.Windows.Forms.Button();
            this.SelectColorDialog = new System.Windows.Forms.ColorDialog();
            this.LoadImageOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SaveImageSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.SampleFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            StyleLabel = new System.Windows.Forms.Label();
            BackgroundColorLable = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.SourcePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ResultPictureBox)).BeginInit();
            this.ResultMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StyleErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RefImageErrorProvider)).BeginInit();
            this.FullPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // StyleLabel
            // 
            StyleLabel.AutoSize = true;
            StyleLabel.Location = new System.Drawing.Point(151, 403);
            StyleLabel.Name = "StyleLabel";
            StyleLabel.Size = new System.Drawing.Size(36, 15);
            StyleLabel.TabIndex = 5;
            StyleLabel.Text = "Type";
            // 
            // BackgroundColorLable
            // 
            BackgroundColorLable.AutoSize = true;
            BackgroundColorLable.Location = new System.Drawing.Point(525, 402);
            BackgroundColorLable.Name = "BackgroundColorLable";
            BackgroundColorLable.Size = new System.Drawing.Size(111, 15);
            BackgroundColorLable.TabIndex = 7;
            BackgroundColorLable.Text = "Background Color";
            // 
            // SourcePictureBox
            // 
            this.SourcePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SourcePictureBox.Location = new System.Drawing.Point(12, 12);
            this.SourcePictureBox.Name = "SourcePictureBox";
            this.SourcePictureBox.Size = new System.Drawing.Size(380, 363);
            this.SourcePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.SourcePictureBox.TabIndex = 0;
            this.SourcePictureBox.TabStop = false;
            // 
            // ResultPictureBox
            // 
            this.ResultPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ResultPictureBox.ContextMenuStrip = this.ResultMenuStrip;
            this.ResultPictureBox.Location = new System.Drawing.Point(398, 12);
            this.ResultPictureBox.Name = "ResultPictureBox";
            this.ResultPictureBox.Size = new System.Drawing.Size(380, 363);
            this.ResultPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ResultPictureBox.TabIndex = 1;
            this.ResultPictureBox.TabStop = false;
            // 
            // ResultMenuStrip
            // 
            this.ResultMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ResultMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ResultSaveButton});
            this.ResultMenuStrip.Name = "ResultMenuStrip";
            this.ResultMenuStrip.Size = new System.Drawing.Size(135, 30);
            // 
            // ResultSaveButton
            // 
            this.ResultSaveButton.Name = "ResultSaveButton";
            this.ResultSaveButton.Size = new System.Drawing.Size(134, 26);
            this.ResultSaveButton.Text = "SaveAs";
            this.ResultSaveButton.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // LoadImageButton
            // 
            this.LoadImageButton.Location = new System.Drawing.Point(23, 381);
            this.LoadImageButton.Name = "LoadImageButton";
            this.LoadImageButton.Size = new System.Drawing.Size(75, 23);
            this.LoadImageButton.TabIndex = 2;
            this.LoadImageButton.Text = "Load";
            this.LoadImageButton.UseVisualStyleBackColor = true;
            this.LoadImageButton.Click += new System.EventHandler(this.LoadImageButton_Click);
            // 
            // StyleComboBox
            // 
            this.StyleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.StyleComboBox.FormattingEnabled = true;
            this.StyleComboBox.Location = new System.Drawing.Point(193, 399);
            this.StyleComboBox.Name = "StyleComboBox";
            this.StyleComboBox.Size = new System.Drawing.Size(199, 23);
            this.StyleComboBox.TabIndex = 4;
            // 
            // StyleErrorProvider
            // 
            this.StyleErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.StyleErrorProvider.ContainerControl = this;
            // 
            // StartProcessButton
            // 
            this.StartProcessButton.Location = new System.Drawing.Point(425, 399);
            this.StartProcessButton.Name = "StartProcessButton";
            this.StartProcessButton.Size = new System.Drawing.Size(75, 23);
            this.StartProcessButton.TabIndex = 2;
            this.StartProcessButton.Text = "Start";
            this.StartProcessButton.UseVisualStyleBackColor = false;
            this.StartProcessButton.Click += new System.EventHandler(this.StartProcessButton_Click);
            // 
            // RefImageErrorProvider
            // 
            this.RefImageErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.RefImageErrorProvider.ContainerControl = this;
            // 
            // FullPanel
            // 
            this.FullPanel.AutoSize = true;
            this.FullPanel.Controls.Add(this.GenerateSampleButton);
            this.FullPanel.Controls.Add(BackgroundColorLable);
            this.FullPanel.Controls.Add(this.SelectBkgCokorButton);
            this.FullPanel.Controls.Add(this.SourcePictureBox);
            this.FullPanel.Controls.Add(StyleLabel);
            this.FullPanel.Controls.Add(this.ResultPictureBox);
            this.FullPanel.Controls.Add(this.StyleComboBox);
            this.FullPanel.Controls.Add(this.LoadSampleButton);
            this.FullPanel.Controls.Add(this.LoadImageButton);
            this.FullPanel.Controls.Add(this.StartProcessButton);
            this.FullPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FullPanel.Location = new System.Drawing.Point(0, 0);
            this.FullPanel.Name = "FullPanel";
            this.FullPanel.Size = new System.Drawing.Size(803, 447);
            this.FullPanel.TabIndex = 6;
            // 
            // GenerateSampleButton
            // 
            this.GenerateSampleButton.Location = new System.Drawing.Point(703, 381);
            this.GenerateSampleButton.Name = "GenerateSampleButton";
            this.GenerateSampleButton.Size = new System.Drawing.Size(75, 54);
            this.GenerateSampleButton.TabIndex = 8;
            this.GenerateSampleButton.Text = "generate samples";
            this.GenerateSampleButton.UseVisualStyleBackColor = true;
            this.GenerateSampleButton.Click += new System.EventHandler(this.GenerateSampleButton_Click);
            // 
            // SelectBkgCokorButton
            // 
            this.SelectBkgCokorButton.BackColor = System.Drawing.Color.Black;
            this.SelectBkgCokorButton.Location = new System.Drawing.Point(652, 398);
            this.SelectBkgCokorButton.Name = "SelectBkgCokorButton";
            this.SelectBkgCokorButton.Size = new System.Drawing.Size(23, 23);
            this.SelectBkgCokorButton.TabIndex = 6;
            this.SelectBkgCokorButton.UseVisualStyleBackColor = false;
            this.SelectBkgCokorButton.Click += new System.EventHandler(this.SelectBkgCokorButton_Click);
            // 
            // LoadSampleButton
            // 
            this.LoadSampleButton.Location = new System.Drawing.Point(23, 412);
            this.LoadSampleButton.Name = "LoadSampleButton";
            this.LoadSampleButton.Size = new System.Drawing.Size(102, 23);
            this.LoadSampleButton.TabIndex = 2;
            this.LoadSampleButton.Text = "Load Sample";
            this.LoadSampleButton.UseVisualStyleBackColor = true;
            this.LoadSampleButton.Click += new System.EventHandler(this.LoadSampleButton_Click);
            // 
            // LoadImageOpenFileDialog
            // 
            this.LoadImageOpenFileDialog.FileName = "openFileDialog1";
            this.LoadImageOpenFileDialog.SupportMultiDottedExtensions = true;
            this.LoadImageOpenFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.LoadImageFromDialog);
            // 
            // SaveImageSaveFileDialog
            // 
            this.SaveImageSaveFileDialog.DefaultExt = "png";
            this.SaveImageSaveFileDialog.SupportMultiDottedExtensions = true;
            // 
            // SampleFolderBrowserDialog
            // 
            this.SampleFolderBrowserDialog.Description = "將範例結果存到...";
            // 
            // TopForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(803, 447);
            this.Controls.Add(this.FullPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "TopForm";
            this.Text = "Paint";
            ((System.ComponentModel.ISupportInitialize)(this.SourcePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ResultPictureBox)).EndInit();
            this.ResultMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.StyleErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RefImageErrorProvider)).EndInit();
            this.FullPanel.ResumeLayout(false);
            this.FullPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox SourcePictureBox;
        private System.Windows.Forms.PictureBox ResultPictureBox;
        private System.Windows.Forms.Button LoadImageButton;
        private System.Windows.Forms.ComboBox StyleComboBox;
        private System.Windows.Forms.ErrorProvider StyleErrorProvider;
        private System.Windows.Forms.Button StartProcessButton;
        private System.Windows.Forms.ErrorProvider RefImageErrorProvider;
        private System.Windows.Forms.Panel FullPanel;
        private System.Windows.Forms.Button SelectBkgCokorButton;
        private System.Windows.Forms.ColorDialog SelectColorDialog;
        private System.Windows.Forms.Button GenerateSampleButton;
        private System.Windows.Forms.ContextMenuStrip ResultMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ResultSaveButton;
        private System.Windows.Forms.Button LoadSampleButton;
        private System.Windows.Forms.OpenFileDialog LoadImageOpenFileDialog;
        private System.Windows.Forms.SaveFileDialog SaveImageSaveFileDialog;
        private System.Windows.Forms.FolderBrowserDialog SampleFolderBrowserDialog;
    }
}

