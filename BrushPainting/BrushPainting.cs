using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrushPainting
{
    public class BrushPainting
    {
        public static Bitmap Generate(Bitmap referenceBitmap, BrushPaintingParams parameters, BrushPaintingParamsEx parameterEX)
        {
            if (!parameterEX.ProcessingSize.IsEmpty && parameterEX.ProcessingSize != referenceBitmap.Size)
            {
                return Generate(new Bitmap(referenceBitmap, parameterEX.ProcessingSize), parameters, parameterEX);
            }
            var sortedStrokes = parameters.BrushRadius.OrderByDescending(x => x);
            Bitmap canvus = new Bitmap(referenceBitmap.Width, referenceBitmap.Height);
            canvus.ClearColor(parameterEX.InitialColor);
            foreach (var strokeRadius in sortedStrokes)
            {
                SuperfastBlur.GaussianBlur blurkernal = new SuperfastBlur.GaussianBlur(referenceBitmap);
                FastBitmap24bppRgb fastreference = FastBitmap24bppRgb.FromBitmap(blurkernal.Process((int)Math.Round(strokeRadius * parameters.BlurFactor)));
                var strokeBeginPoints = GenerateStrokeBeginPoints(FastBitmap24bppRgb.FromBitmap(canvus), fastreference, (int)Math.Round(parameters.GridFactor * strokeRadius), parameters.Threshold);

                StrokeGenerator strokeGenerator = new StrokeGenerator(fastreference, FastBitmap24bppRgb.FromBitmap(canvus));
                strokeGenerator.MinStrokeLength = parameters.MinimumStrokeLength;
                strokeGenerator.MaxStrokeLength = parameters.MaximumStrokeLengths;
                strokeGenerator.Weight = parameters.CurvatureFilter;
                var strokes = strokeBeginPoints.Shuffle().Select(p => strokeGenerator.GenerateStroke(p, strokeRadius)).ToList();
                PaintStrokes(canvus, fastreference, strokeRadius, strokes, (byte)Math.Round(parameters.Opacity * 255));
            }

            return canvus;
        }

        private static void PaintStrokes(Bitmap canvus, FastBitmap24bppRgb referenceImage, int r, List<List<Point>> strokes, byte alpha = 255)
        {
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(alpha, 0, 0, 0)))
            using (Pen pen = new Pen(Color.FromArgb(alpha, 255, 255, 255), r * 2))
            using (Graphics g = Graphics.FromImage(canvus))
            {
                pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;
                pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                foreach (var s in strokes)
                {
                    Debug.Assert(s.Count >= 1);
                    Color color = Color.FromArgb(alpha, referenceImage.GetPixel(s[0].X, s[0].Y));
                    brush.Color = color;

                    if (s.Count == 1)
                    {
                        brush.Color = color;
                        g.FillEllipse(brush, new Rectangle(s[0].X - r, s[0].Y - r, r * 2, r * 2));
                    }
                    else if (s.Count == 2)
                    {
                        pen.Color = color;
                        g.DrawLine(pen, s[0], s[1]);
                    }
                    else
                    {
                        pen.Color = color;
                        g.DrawCurve(pen, s.ToArray());
                    }
                }
            }
        }

        //CANPARALLEL

        private static IEnumerable<Point> GenerateStrokeBeginPoints(FastBitmap24bppRgb imageA, FastBitmap24bppRgb imageB, int stepSize, int threshold)
        {
            if (stepSize == 0)
                stepSize = 1;
            int radiusInclusive = stepSize / 2;

            int width = imageA.Width;
            int height = imageA.Height;
            threshold = threshold * threshold;

            HashSet<Point> points = new HashSet<Point>();
            var distance = ImageOperations.DistanceSquare(imageA, imageB);
            var diff = (int[,])distance.Clone();

            //accumulate
            for (int j = 0; j < height; ++j)
            {
                for (int i = 1; i < width; ++i)
                {
                    diff[i, j] += diff[i - 1, j];
                }
            }
            for (int j = 1; j < height; ++j)
            {
                for (int i = 0; i < width; ++i)
                {
                    diff[i, j] += diff[i, j - 1];
                }
            }

            for (int y = 0; y < height; y += stepSize)
            {
                for (int x = 0; x < width; x += stepSize)
                {
                    int minx = Math.Max(0, x - radiusInclusive);
                    int maxx = Math.Min(width - 1, x + radiusInclusive);
                    int miny = Math.Max(0, y - radiusInclusive);
                    int maxy = Math.Min(height - 1, y + radiusInclusive);
                    double avgdistance =
                        diff[maxx, maxy]
                        + (minx == 0 || miny == 0 ? 0 : diff[minx - 1, miny - 1])
                        - (minx == 0 ? 0 : diff[minx - 1, maxy])
                        - (miny == 0 ? 0 : diff[maxx, miny - 1]);
                    avgdistance /= (maxx - minx + 1) * (maxy - miny + 1);
                    if (avgdistance >= threshold)
                    {
                        double max = 0;
                        int xx = x;
                        int yy = y;

                        for (int i = minx; i <= maxx; ++i)
                        {
                            for (int j = miny; j <= maxy; ++j)
                            {
                                if (distance[i, j] > max)
                                {
                                    max = distance[i, j];
                                    xx = i;
                                    yy = j;
                                }
                            }
                        }
                        points.Add(new Point(xx, yy));
                    }
                }
            }
            return points;
        }


        [Conditional("GENERATEPPTPIC")]
        private static void GenerateIntermidiatePicture(FastBitmap24bppRgb referenceImage, int step, IEnumerable<Point> strokeBeginPoints)
        {
            FastBitmap24bppRgb gray = new FastBitmap24bppRgb(referenceImage);
            var grayarray = ImageOperations.Luminance(gray);
            for (int i = 0; i < gray.Width; ++i)
            {
                for (int j = 0; j < gray.Height; ++j)
                {
                    gray.SetPixel(i, j, grayarray[i, j], grayarray[i, j], grayarray[i, j]);
                }
            }
            gray.ToBitmap().Save("gray.bmp");

            Bitmap grid = gray.ToBitmap();
            using (Graphics g = Graphics.FromImage(grid))
            {
                for (int i = step / 2; i < grid.Width; i += step)
                {
                    g.DrawLine(Pens.PaleVioletRed, new Point(i, 0), new Point(i, referenceImage.Height));
                }
                for (int i = step / 2; i < grid.Height; i += step)
                {
                    g.DrawLine(Pens.PaleVioletRed, new Point(0, i), new Point(referenceImage.Width, i));
                }
            }
            grid.Save("grid.bmp");

            Bitmap startPoint = grid;//meant to use reference (alias to grid)
            using (Graphics g = Graphics.FromImage(startPoint))
            {
                foreach (var point in strokeBeginPoints)
                {
                    g.FillRectangle(Brushes.Aqua, point.X, point.Y, 1, 1);
                }
            }
            startPoint.Save("beginPoints.bmp");

            int[,] sobelX, sobelY;
            ImageOperations.Sobel(referenceImage, out sobelX, out sobelY);
            float max = 0;
            foreach (int i in sobelX)
                max = Math.Max(max, i);
            max = 255 / max;
            for (int i = 0; i < gray.Width; ++i)
            {
                for (int j = 0; j < gray.Height; ++j)
                {
                    gray.SetPixel(i, j, (byte)(255 - sobelX[i, j] * max), (byte)(255 - sobelX[i, j] * max), (byte)(255 - sobelX[i, j] * max));
                }
            }
            gray.ToBitmap().Save("sobelX.bmp");

            foreach (int i in sobelY)
                max = Math.Max(max, i);
            max = 255 / max;
            for (int i = 0; i < gray.Width; ++i)
            {
                for (int j = 0; j < gray.Height; ++j)
                {
                    gray.SetPixel(i, j, (byte)(255 - sobelY[i, j] * max), (byte)(255 - sobelY[i, j] * max), (byte)(255 - sobelY[i, j] * max));
                }
            }
            gray.ToBitmap().Save("sobelY.bmp");

            int[,] sobelXY = (int[,])sobelX.Clone();
            for (int i = 0; i < gray.Width; ++i)
            {
                for (int j = 0; j < gray.Height; ++j)
                {
                    sobelXY[i, j] = (int)Math.Sqrt(sobelY[i, j] * sobelY[i, j] + sobelX[i, j] * sobelX[i, j]);
                }
            }
            foreach (int i in sobelXY)
                max = Math.Max(max, i);
            max = 255 / max;
            for (int i = 0; i < gray.Width; ++i)
            {
                for (int j = 0; j < gray.Height; ++j)
                {
                    gray.SetPixel(i, j, (byte)(255 - sobelXY[i, j] * max), (byte)(255 - sobelXY[i, j] * max), (byte)(255 - sobelXY[i, j] * max));
                }
            }
            gray.ToBitmap().Save("sobelXY.bmp");
        }
    }
}
