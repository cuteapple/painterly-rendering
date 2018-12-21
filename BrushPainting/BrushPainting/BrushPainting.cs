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
    public class BrushPaintingProcess
    {
        public static Bitmap Generate(Bitmap referenceBitmap, BrushPaintingParams parameters, BrushPaintingParamsEx parameterEX)
        {
            //Check If Resize Needed
            referenceBitmap = ResizeBitmap(referenceBitmap, parameterEX);
            var sortedStrokesRadius = parameters.BrushRadius.OrderByDescending(x => x);
            Bitmap canvus = new Bitmap(referenceBitmap.Width, referenceBitmap.Height);
            canvus.ClearColor(parameterEX.InitialColor);
            foreach (var strokeRadius in sortedStrokesRadius)
            {
                SuperfastBlur.GaussianBlur blurkernal = new SuperfastBlur.GaussianBlur(referenceBitmap);
                FastBitmap24bppRgb fastreference = FastBitmap24bppRgb.FromBitmap(blurkernal.Process((int)Math.Round(strokeRadius * parameters.BlurFactor)));
                var strokeBeginPoints = GenerateStrokeBeginPoints(FastBitmap24bppRgb.FromBitmap(canvus), fastreference, (int)Math.Round(parameters.GridFactor * strokeRadius), parameters.Threshold);

                StrokeGenerator strokeGenerator = new StrokeGenerator(fastreference, FastBitmap24bppRgb.FromBitmap(canvus));
                strokeGenerator.MinStrokeLength = parameters.MinimumStrokeLength;
                strokeGenerator.MaxStrokeLength = parameters.MaximumStrokeLengths;
                strokeGenerator.Weight = parameters.CurvatureFilter;

                Random random = parameterEX.random ?? new Random();
                var strokes = strokeBeginPoints.Shuffle(random).Select(p => strokeGenerator.GenerateStroke(p, strokeRadius)).ToList();
                PaintStrokes(canvus, fastreference, strokeRadius, strokes, (byte)Math.Round(parameters.Opacity * 255), parameters.ColorJitter);
            }
            return canvus;
        }

        private static Bitmap ResizeBitmap(Bitmap referenceBitmap, BrushPaintingParamsEx parameterEX)
        {
            if ((parameterEX.ProcessingSize_Width > 0) && parameterEX.ProcessingSize_Width != referenceBitmap.Width)
            {
                int height = referenceBitmap.Height * parameterEX.ProcessingSize_Width / referenceBitmap.Width;
                if (height <= 0)
                    height = 1;
                referenceBitmap = new Bitmap(referenceBitmap, parameterEX.ProcessingSize_Width, height);
            }
            return referenceBitmap;
        }

        private static void PaintStrokes(Bitmap canvus, FastBitmap24bppRgb referenceImage, int r, List<List<Point>> strokes, byte alpha, ColorJitter jitter)
        {
            var detailStrokeList = strokes.Select(s => new Stroke()
                {
                    Color = jitter.Apply(Color.FromArgb(alpha, referenceImage.GetPixel(s[0].X, s[0].Y))),
                    Points = s,
                    Radius = r
                });
            PaintStrokes(canvus, detailStrokeList);
        }

        private static void PaintStrokes(Bitmap canvus, IEnumerable<Stroke> strokes)
        {
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(0, 0, 0, 0)))
            using (Pen pen = new Pen(Color.FromArgb(0, 255, 255, 255), 1))
            using (Graphics g = Graphics.FromImage(canvus))
            {
                pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;
                pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                foreach (var s in strokes)
                {
                    switch (s.Points.Count)
                    {
                        case 0:
                            continue;
                        case 1:
                            brush.Color = s.Color;
                            g.FillEllipse(brush, new Rectangle(s.Points[0].X - s.Radius, s.Points[0].Y - s.Radius, s.Radius * 2, s.Radius * 2));
                            break;
                        case 2:
                            pen.Color = s.Color;
                            pen.Width = s.Radius * 2;
                            g.DrawLine(pen, s.Points[0], s.Points[1]);
                            break;
                        default:
                            pen.Color = s.Color;
                            pen.Width = s.Radius * 2;
                            g.DrawCurve(pen, s.Points.ToArray());
                            break;
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
    }
}
