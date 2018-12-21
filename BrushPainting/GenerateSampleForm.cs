using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace BrushPainting
{
    public partial class GenerateSampleForm : Form
    {
        private Bitmap source;
        private BrushPaintingParams param;
        private BrushPaintingParamsEx paramEx;
        private string directory;
        ManualResetEvent stopEvent = new ManualResetEvent(false);
        private Task task;

        public GenerateSampleForm(Bitmap source, BrushPaintingParams param, BrushPaintingParamsEx paramsEx, string directory)
        {
            InitializeComponent();
            this.source = source.Clone() as Bitmap;
            this.param = param;
            this.paramEx = paramsEx;
            this.directory = directory;
            task = ProcessStart().ContinueWith(x => stopEvent.Close());
        }

        private void GenerateSampleForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!task.IsCompleted)
            {
                var result = MessageBox.Show("確定要終止嗎？", "!!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }

                stopEvent.Set();
            }
        }

        private async Task ProcessStart()
        {
            string oritext = this.Text;
            this.Text = "Generating Samples";

            var nspair = GetNameStylePairs();
            GeneratingProgressBar.Maximum = nspair.Count();
            GeneratingProgressBar.Value = 0;

            Bitmap bmp = this.source;
            BrushPaintingParamsEx paramEx = this.paramEx;

            Action IncreaceProgress = new Action(() => ++GeneratingProgressBar.Value);
            Action<Bitmap> UpdateBitmap = new Action<Bitmap>((b) => this.CurrentPictureBox.Image = b);
            Action<string> UpdateBitmapDescription = new Action<string>((s) => this.CurrentPictureNameLabel.Text = s);

            foreach (var p in nspair)
            {
                if (stopEvent.WaitOne(0))
                {
                    return;
                }
                var result = await Task.Run(() => BrushPaintingProcess.Generate(bmp, p.Parameters, paramEx.Clone()));
                if (stopEvent.WaitOne(0))
                {
                    return;
                }
                result.Save(directory + "/" + p.Name + ".png");
                Invoke(IncreaceProgress);
                Invoke(UpdateBitmap, result);
                Invoke(UpdateBitmapDescription, p.Name);
            }

            this.Text = "Done";
        }

        private class NameStylePair
        {
            public string Name;
            public BrushPaintingParams Parameters;
        }

        private IEnumerable<NameStylePair> GetNameStylePairs()
        {
            var DoubleRange_0_1 = Enumerable.Range(0, 21).Select(x => x * 0.05).ToList();
            int MaxThreasHold = (int)Math.Sqrt(255 * 255 * 3);
            var ByteRange_0_255 = Enumerable.Range(0, MaxThreasHold / 20).Select(x => x * 20).Concat(new int[] { MaxThreasHold }).ToList();
            var result = new NameStylePair();
            string format = "0.00";

            result.Parameters = param.Clone();
            foreach (var o in DoubleRange_0_1)
            {
                result.Parameters.Opacity = o;
                result.Name = @"Opacity ： " + o.ToString(format);
                yield return result;
            }

            result.Parameters = param.Clone();
            foreach (var t in ByteRange_0_255)
            {
                result.Parameters.Threshold = t;
                result.Name = @"Threshold ： " + t.ToString("000");
                yield return result;
            }

            result.Parameters = param.Clone();
            foreach (var c in DoubleRange_0_1)
            {
                result.Parameters.CurvatureFilter = c;
                result.Name = @"Curvature ： " + c.ToString(format);
                yield return result;
            }

            result.Parameters = param.Clone();
            foreach (var o in DoubleRange_0_1)
            {
                result.Parameters.ColorJitter = new HSVColorJitter(0, 0, o);
                result.Name = @"Jitter(V) ： " + o.ToString(format);
                yield return result;
            }

            result.Parameters = param.Clone();
            foreach (var o in DoubleRange_0_1)
            {
                result.Parameters.ColorJitter = new HSVColorJitter(o, 0, 0);
                result.Name = @"Jitter(H) ： " + o.ToString(format);
                yield return result;
            }

            result.Parameters = param.Clone();
            foreach (var o in DoubleRange_0_1)
            {
                result.Parameters.ColorJitter = new HSVColorJitter(0, o, 0);
                result.Name = @"Jitter(S) ： " + o.ToString(format);
                yield return result;
            }

            result.Parameters = param.Clone();
            foreach (var o in DoubleRange_0_1)
            {
                result.Parameters.ColorJitter = new RGBColorJitter(o, 0, 0);
                result.Name = @"Jitter(R) ： " + o.ToString(format);
                yield return result;
            }

            result.Parameters = param.Clone();
            foreach (var o in DoubleRange_0_1)
            {
                result.Parameters.ColorJitter = new RGBColorJitter(0, o, 0);
                result.Name = @"Jitter(G) ： " + o.ToString(format);
                yield return result;
            }

            result.Parameters = param.Clone();
            foreach (var o in DoubleRange_0_1)
            {
                result.Parameters.ColorJitter = new RGBColorJitter(0, 0, o);
                result.Name = @"Jitter(B) ： " + o.ToString(format);
                yield return result;
            }
        }
    }
}
