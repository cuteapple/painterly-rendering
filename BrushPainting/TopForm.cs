using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace BrushPainting
{
    public partial class TopForm : Form
    {
        private Bitmap _referenceBitmap = null;
        private Bitmap ReferenceBitmap
        {
            get
            {
                return _referenceBitmap;
            }
            set
            {
                _referenceBitmap = value;
                SourcePictureBox.Image = value;
            }
        }

        private Bitmap _resultBitmap = null;
        private Bitmap ResultBitmap
        {
            get
            {
                return _resultBitmap;
            }
            set
            {
                _resultBitmap = value;
                ResultPictureBox.Image = value;
            }
        }

        private BrushPaintingParams SelectedStyle
        {
            get
            {
                var property = typeof(BrushPaintingParamsDefaultValus).GetProperty(StyleComboBox.Text, BindingFlags.Public | BindingFlags.Static);
                if (property == null)
                {
                    return null;
                }
                return property.GetValue(null) as BrushPaintingParams;
            }
        }

        private BrushPaintingParamsEx SelectedStyleEx
        {
            get
            {
                return new BrushPaintingParamsEx
                {
                    ProcessingSize_Width = 640,
                    InitialColor = SelectColorDialog.Color,
                    random = new Random()
                };
            }
        }

        public TopForm()
        {
            InitializeComponent();
            //Get Style Choices And Add To ComboBox
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            var styleNameList = typeof(BrushPaintingParamsDefaultValus)
                .GetProperties(BindingFlags.Static | BindingFlags.Public)
                .Select(x => x.Name)
                .ToArray();
            StyleComboBox.Items.AddRange(styleNameList);
            if (styleNameList.Length != 0)
            {
                StyleComboBox.SelectedIndex = 0;
            }
        }

        private void LoadImageButton_Click(object sender, EventArgs e)
        {
            LoadImageOpenFileDialog.ShowDialog();
        }


        private async void StartProcessButton_Click(object sender, EventArgs e)
        {
            bool pass = CheckArgument();
            if (!pass)
            {
                MessageBox.Show("Invalid Parameters");
                return;
            }
            await ProcessAndShowResult();
        }

        private bool CheckArgument()
        {
            bool pass = true;
            if (ReferenceBitmap == null)
            {
                RefImageErrorProvider.SetError(LoadImageButton, "Invalid Image");
                pass = false;
            }
            else
            {
                RefImageErrorProvider.Clear();
            }
            if (SelectedStyle == null || SelectedStyleEx == null)
            {
                StyleErrorProvider.SetError(StyleComboBox, "Invalid Style");
                pass = false;
            }
            else
            {
                StyleErrorProvider.Clear();
            }
            return pass;
        }

        private async Task ProcessAndShowResult()
        {
            string oritext = this.Text;
            this.Text = "Processing";
            FullPanel.Enabled = false;

            Bitmap bmp = ReferenceBitmap;
            BrushPaintingParams param = SelectedStyle;
            BrushPaintingParamsEx paramEx = SelectedStyleEx;
            ResultBitmap = await Task.Run(() => BrushPaintingProcess.Generate(bmp, param, paramEx));

            FullPanel.Enabled = true;
            this.Text = oritext;
        }

        private static Bitmap ReadBitmapWithoutFileLock(string file)
        {
            byte[] data = File.ReadAllBytes(file);
            MemoryStream ms = new MemoryStream(data);
            try
            {
                return new Bitmap(ms);
            }
            catch
            {
                return null;
            }
        }

        private void SelectBkgCokorButton_Click(object sender, EventArgs e)
        {
            var result = SelectColorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                SelectBkgCokorButton.BackColor = SelectColorDialog.Color;
            }
        }

        private void GenerateSampleButton_Click(object sender, EventArgs e)
        {
            bool pass = CheckArgument();
            if (!pass)
            {
                return;
            }

            var result = SampleFolderBrowserDialog.ShowDialog();
            if (result != System.Windows.Forms.DialogResult.OK || !Directory.Exists(SampleFolderBrowserDialog.SelectedPath))
            {
                return;
            }

            string directory = new DirectoryInfo(SampleFolderBrowserDialog.SelectedPath).FullName;

            GenerateSampleForm gform = new GenerateSampleForm(ReferenceBitmap, SelectedStyle, SelectedStyleEx, directory);
            gform.ShowDialog();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ResultBitmap == null)
            {
                return;
            }
            SaveFileDialog sd = new SaveFileDialog()
            {
                OverwritePrompt = true,
                ValidateNames = true
            };
            if (sd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            try
            {
                ResultBitmap.Save(sd.FileName);
            }
            catch
            {
                MessageBox.Show(String.Format("Fail To Write To \"{0}\""), sd.FileName);
            }
        }

        private void LoadSampleButton_Click(object sender, EventArgs e)
        {
            this.ReferenceBitmap = new Bitmap(BrushPainting.Properties.Resources.pumpkin);
        }

        private void LoadImageFromDialog(object sender, CancelEventArgs e)
        {
            var image = ReadBitmapWithoutFileLock(LoadImageOpenFileDialog.FileName);
            if (image == null)
            {
                RefImageErrorProvider.SetError(LoadImageButton, "Invalid Image");
                return;
            }
            this.ReferenceBitmap = image;
        }

    }
}
