using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrushPainting
{
    public class StrokeGenerator
    {
        public int MaxStrokeLength
        {
            get;
            set;
        }
        public int MinStrokeLength
        {
            get;
            set;
        }
        public double Weight
        {
            get;
            set;
        }
        private int width;
        private int height;
        private int[,] gradientX;
        private int[,] gradientY;
        //private Point[,] offset; //= gradientY,gradientX  normalize
        private FastBitmap24bppRgb reference;
        private FastBitmap24bppRgb target;

        public StrokeGenerator(FastBitmap24bppRgb reference, FastBitmap24bppRgb target)
        {
            this.MaxStrokeLength = 16;
            this.MinStrokeLength = 0;
            this.Weight = 1.0;
            this.reference = new FastBitmap24bppRgb(reference);
            this.target = new FastBitmap24bppRgb(target);
            this.width = reference.Width;
            this.height = reference.Height;
            ImageOperations.Sobel(reference, out gradientX, out gradientY);
        }

        public List<Point> GenerateStroke(Point begin, int r)
        {
            List<Point> result = new List<Point>();
            result.Add(begin);
            //return result;
            Color strokeColor = reference.GetPixel(begin.X, begin.Y);

            int x = begin.X, y = begin.Y;
            int lastdx = 0, lastdy = 0;
            for (int i = 0; i <= MaxStrokeLength; ++i)
            {
                if (gradientX[x, y] == 0 && gradientY[x, y] == 0)
                    break;//MinStrokeLength becomes meaningless?

                if (i >= MinStrokeLength)
                {
                    var colorref = reference.GetPixel(x, y);
                    var colorcvs = target.GetPixel(x, y);
                    int disref = ImageOperations.DistanceSquareRGB(colorref, strokeColor);
                    int discvs = ImageOperations.DistanceSquareRGB(colorcvs, strokeColor);
                    if (discvs < disref)
                        break;
                }

                #region Improve
                //Only Used For First
                if (lastdx == 0 && lastdy == 0)
                {

                }
                #endregion

                var dx = -gradientY[x, y];
                var dy = gradientX[x, y];
                if (lastdx * dx + lastdy * dy < 0)
                {
                    dx = -dx;
                    dy = -dy;
                }

                double lengthscale = r / Math.Sqrt(dx * dx + dy * dy);
                dx = (int)(dx * lengthscale);
                dy = (int)(dy * lengthscale);

                dx = (int)(Weight * dx + (1 - Weight) * lastdx);
                dy = (int)(Weight * dy + (1 - Weight) * lastdy);

                if (dx == 0 && dy == 0)
                    break;

                //in case last is less than one
                lengthscale = r / Math.Sqrt(dx * dx + dy * dy);
                dx = (int)(dx * lengthscale);
                dy = (int)(dy * lengthscale);

                #region Improve
                var newx = x + dx;
                var newy = y + dy;

                //TODO: use raycast to box to get better accuracy
                if (newx < 0)
                    newx = 0;
                if (newy < 0)
                    newy = 0;
                if (newx >= width)
                    newx = width - 1;
                if (newy >= height)
                    newy = height - 1;

                dx = newx - x;
                dy = newy - y;

                if (dx == 0 && dy == 0)
                    break;

                x = newx;
                y = newy;
                #endregion

                //in case last is less than zero
                lengthscale = r / Math.Sqrt(dx * dx + dy * dy);
                dx = (int)(dx * lengthscale);
                dy = (int)(dy * lengthscale);

                lastdx = dx;
                lastdy = dy;
                result.Add(new Point(x, y));
            }

            return result;
        }
    }
}
