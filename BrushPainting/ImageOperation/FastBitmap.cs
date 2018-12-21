using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace BrushPainting
{
    public class FastBitmap24bppRgb
    {
        #region Constructor
        private FastBitmap24bppRgb(BitmapData bmpdata)
        {
            Width = bmpdata.Width;
            Height = bmpdata.Height;
            data = new byte[bmpdata.Stride * bmpdata.Height];
            stride = bmpdata.Stride;
            Marshal.Copy(bmpdata.Scan0, data, 0, data.Length);
        }

        public FastBitmap24bppRgb(FastBitmap24bppRgb origin)
        {
            this.data = (byte[])origin.data.Clone();
            this.Width = origin.Width;
            this.Height = origin.Height;
            this.stride = origin.stride;
        }

        public static FastBitmap24bppRgb FromBitmap(Bitmap source)
        {
            FastBitmap24bppRgb fbmp24;
            using (Bitmap bitmap = new Bitmap(source.Width, source.Height, PixelFormat.Format24bppRgb))
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
                g.DrawImage(source, 0, 0, source.Width, source.Height);
                var bmpdata = bitmap.LockBits(new Rectangle(0, 0, source.Width, source.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                fbmp24 = new FastBitmap24bppRgb(bmpdata);
                bitmap.UnlockBits(bmpdata);
            }
            return fbmp24;
        }

        public Bitmap ToBitmap()
        {
            Bitmap bmp = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
            var bmpdata = bmp.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            Marshal.Copy(data, 0, bmpdata.Scan0, data.Length);
            bmp.UnlockBits(bmpdata);
            return bmp;
        }
        /*
        public void Save(string filename)
        {
            unsafe
            {
                fixed (byte* scan0 = data)
                {
                    using (Bitmap bmp = new Bitmap(Width, Height, stride, PixelFormat.Format24bppRgb, new IntPtr(scan0)))
                    {
                        bmp.Save(filename);
                    }
                }
            }
        }*/
        #endregion

        public enum ColorChannel
        {
            Blue = 0,
            Green = 1,
            Red = 2,
            R = Red,
            G = Green,
            B = Blue
        }
        private byte[] data;
        private int stride;
        public int Width
        {
            get;
            private set;
        }
        public int Height
        {
            get;
            private set;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte GetPixel(ColorChannel channel, int x, int y)
        {
            return data[stride * y + x * 3 + (int)channel];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetPixel(ColorChannel channel, int x, int y, byte value)
        {
            data[stride * y + x * 3 + (int)channel] = value;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetPixel(int x, int y, byte R, byte G, byte B)
        {
            int scan0 = stride * y + x * 3;
            data[scan0 + (int)ColorChannel.Red] = R;
            data[scan0 + (int)ColorChannel.Green] = G;
            data[scan0 + (int)ColorChannel.Blue] = B;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetPixel(int x, int y, out byte R, out byte G, out byte B)
        {
            int scan0 = stride * y + x * 3;
            R = data[scan0 + (int)ColorChannel.Red];
            G = data[scan0 + (int)ColorChannel.Green];
            B = data[scan0 + (int)ColorChannel.Blue];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Color GetPixel(int x, int y)
        {
            byte R, G, B;
            GetPixel(x, y, out R, out G, out B);
            return Color.FromArgb(R, G, B);
        }
    }
}
