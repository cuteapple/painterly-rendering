using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrushPainting
{
    public static class GaussianBlur
    {
        public static Bitmap PerfectGaussianBlur(Bitmap sample, double sigma)
        {
            int width = sample.Width;
            int height = sample.Height;
            FastBitmap24bppRgb source = FastBitmap24bppRgb.FromBitmap(sample);
            FastBitmap24bppRgb dest = FastBitmap24bppRgb.FromBitmap(new Bitmap(width, height, PixelFormat.Format24bppRgb));

            GaussianKernal kernal = new GaussianKernal(sigma);

            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    int beginX = Math.Max(x - kernal.Radius, 0);
                    int endX = Math.Min(x + kernal.Radius, width);
                    int beginY = Math.Max(y - kernal.Radius, 0);
                    int endY = Math.Min(y + kernal.Radius, height);

                    double r = 0, g = 0, b = 0, scaleSum = 0;
                    for (int iy = beginY; iy < endY; ++iy)
                    {
                        for (int ix = beginX; ix < endX; ++ix)
                        {
                            double scale = kernal[ix - x, iy - y];
                            scaleSum += scale;
                            r += source.GetPixel(FastBitmap24bppRgb.ColorChannel.Red, ix, iy) * scale;
                            g += source.GetPixel(FastBitmap24bppRgb.ColorChannel.Green, ix, iy) * scale;
                            b += source.GetPixel(FastBitmap24bppRgb.ColorChannel.Blue, ix, iy) * scale;
                        }
                    }
                    r /= scaleSum;
                    g /= scaleSum;
                    b /= scaleSum;
                    dest.SetPixel(x, y, (byte)r, (byte)g, (byte)b);
                }
            }

            return dest.ToBitmap();
        }

        public static Bitmap DoNothing(Bitmap sample, double sigma=1)
        {
            int width = sample.Width;
            int height = sample.Height;
            FastBitmap24bppRgb source = FastBitmap24bppRgb.FromBitmap(sample);
            FastBitmap24bppRgb dest = FastBitmap24bppRgb.FromBitmap(new Bitmap(width, height, PixelFormat.Format24bppRgb));

            GaussianKernal kernal = new GaussianKernal(sigma);

            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {

                    dest.SetPixel(x, y, 
                        (byte)source.GetPixel(FastBitmap24bppRgb.ColorChannel.R, x, y),
                        (byte)source.GetPixel(FastBitmap24bppRgb.ColorChannel.G, x, y), 
                        (byte)source.GetPixel(FastBitmap24bppRgb.ColorChannel.B, x, y));
                }
            }

            return dest.ToBitmap();
        }
    }
}
