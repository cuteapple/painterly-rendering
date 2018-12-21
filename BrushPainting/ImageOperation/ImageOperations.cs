using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BrushPainting
{
    public static class ImageOperations
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int DistanceSquareRGB(Color A, Color B)
        {
            int dr = A.R - B.R;
            int dg = A.G - B.G;
            int db = A.B - B.B;
            return dr * dr + dg * dg + db * db;
        }

        public static int[,] DistanceSquare(FastBitmap24bppRgb bmp1, FastBitmap24bppRgb bmp2)
        {
            int[,] result = new int[bmp1.Width, bmp1.Height];
            for (int x = 0; x < bmp1.Width; ++x)
            {
                for (int y = 0; y < bmp1.Height; ++y)
                {
                    int R = (int)bmp1.GetPixel(FastBitmap24bppRgb.ColorChannel.R, x, y) - (int)bmp2.GetPixel(FastBitmap24bppRgb.ColorChannel.R, x, y);
                    int G = (int)bmp1.GetPixel(FastBitmap24bppRgb.ColorChannel.G, x, y) - (int)bmp2.GetPixel(FastBitmap24bppRgb.ColorChannel.G, x, y);
                    int B = (int)bmp1.GetPixel(FastBitmap24bppRgb.ColorChannel.B, x, y) - (int)bmp2.GetPixel(FastBitmap24bppRgb.ColorChannel.B, x, y);
                    result[x, y] = R * R + G * G + B * B;
                }
            }
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static byte LuminanceFormula(byte R, byte G, byte B)
        {
            return (byte)(0.3 * R + 0.59 * G + 0.11 * B);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte Clamp(int value)
        {

            if (value < 0)
                value = 0;
            if (value > 255)
                value = 255;
            return (byte)value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte Clamp(double value)
        {

            if (value < 0)
                value = 0;
            if (value > 255)
                value = 255;
            return (byte)Math.Round(value);
        }

        public static byte[,] Luminance(FastBitmap24bppRgb bmp)
        {
            byte[,] result = new byte[bmp.Width, bmp.Height];
            for (int y = 0; y < bmp.Height; ++y)
            {
                for (int x = 0; x < bmp.Width; ++x)
                {
                    byte R, G, B;
                    bmp.GetPixel(x, y, out R, out G, out B);
                    result[x, y] = LuminanceFormula(R, G, B);
                }
            }
            return result;
        }

        private static byte[,] Extend(byte[,] source, int borderWidth)
        {
            int width = source.GetLength(0);
            int height = source.GetLength(1);
            byte[,] result = new byte[width + 2, height + 2];
            for (int x = 0; x < width; ++x)
            {
                for (int y = 0; y < height; ++y)
                {
                    result[x + borderWidth, y + borderWidth] = source[x, y];
                }
            }
            return result;
        }

        public static void Sobel(FastBitmap24bppRgb referenceImage, out int[,] X, out int[,] Y)
        {
            var extlumin = Extend(Luminance(referenceImage), 1);
            int width = referenceImage.Width;
            int height = referenceImage.Height;
            X = new int[width, height];
            Y = new int[width, height];
            for (int x = 1; x <= width; ++x)
            {
                for (int y = 1; y <= height; ++y)
                {
                    X[x - 1, y - 1] =
                        extlumin[x + 1, y - 1] + (int)extlumin[x + 1, y] * 2 + extlumin[x + 1, y + 1]
                        - extlumin[x - 1, y - 1] - (int)extlumin[x - 1, y] * 2 - extlumin[x - 1, y + 1];
                    Y[x - 1, y - 1] =
                        extlumin[x - 1, y + 1] + (int)extlumin[x, y + 1] * 2 + extlumin[x + 1, y + 1]
                        - extlumin[x - 1, y - 1] - (int)extlumin[x, y - 1] * 2 - extlumin[x + 1, y - 1];
                }
            }
        }

        public static FastBitmap24bppRgb CreateBitmapFrom<T>(T[,] arr, Func<T, Color> converter)
        {
            int width = arr.GetLength(0);
            int height = arr.GetLength(1);
            FastBitmap24bppRgb fastbmp = FastBitmap24bppRgb.FromBitmap(new Bitmap(width, height));
            for (int i = 0; i < width; ++i)
            {
                for (int j = 0; j < height; ++j)
                {
                    Color color = converter(arr[i, j]);
                    fastbmp.SetPixel(i, j, color.R, color.G, color.B);
                }
            }
            return fastbmp;
        }
        public static Color HSVToColor(double H, double S, double V)
        {
            while (H < 0)
            {
                H += 360;
            };
            while (H >= 360)
            {
                H -= 360;
            };
            double R, G, B;
            if (V <= 0)
            {
                R = G = B = 0;
            }
            else if (S <= 0)
            {
                R = G = B = V;
            }
            else
            {
                double hf = H / 60.0;
                int i = (int)Math.Floor(hf);
                double f = hf - i;
                double pv = V * (1 - S);
                double qv = V * (1 - S * f);
                double tv = V * (1 - S * (1 - f));
                switch (i)
                {

                    // Red is the dominant color

                    case 0:
                        R = V;
                        G = tv;
                        B = pv;
                        break;

                    // Green is the dominant color

                    case 1:
                        R = qv;
                        G = V;
                        B = pv;
                        break;
                    case 2:
                        R = pv;
                        G = V;
                        B = tv;
                        break;

                    // Blue is the dominant color

                    case 3:
                        R = pv;
                        G = qv;
                        B = V;
                        break;
                    case 4:
                        R = tv;
                        G = pv;
                        B = V;
                        break;

                    // Red is the dominant color

                    case 5:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.

                    case 6:
                        R = V;
                        G = tv;
                        B = pv;
                        break;
                    case -1:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // The color is not defined, we should throw an error.

                    default:
                        //LFATAL("i Value error in Pixel conversion, Value is %d", i);
                        R = G = B = V; // Just pretend its black/white
                        break;
                }
            }
            var r = Clamp(R * 255.0);
            var g = Clamp(G * 255.0);
            var b = Clamp(B * 255.0);
            return Color.FromArgb(r, g, b);
        }

        public static void ColorToHSV(Color color, out double hue, out double saturation, out double value)
        {
            int max = Math.Max(color.R, Math.Max(color.G, color.B));
            int min = Math.Min(color.R, Math.Min(color.G, color.B));

            hue = color.GetHue();
            saturation = (max == 0) ? 0 : 1d - (1d * min / max);
            value = max / 255d;
        }
    }
}
