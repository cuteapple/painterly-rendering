using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrushPainting
{
    public class GaussianKernal
    {
        GaussianKernal1D kernal;
        public GaussianKernal(double sigma)
        {
            kernal = new GaussianKernal1D(sigma);
        }
        public int Radius
        {
            get
            {
                return kernal.Radius;
            }
        }
        public double this[int x, int y]
        {
            get
            {
                return kernal[x] * kernal[y];
            }
        }
    }

    public class GaussianKernal1D
    {
        private double[] coefficient;
        public GaussianKernal1D(double sigma)
        {
            coefficient = new double[(int)Math.Ceiling(sigma * 3)];
            for (int i = 0; i < coefficient.Length; ++i)
            {
                coefficient[i] = GaussianKernal1D.Calculate(sigma, i);
            }
        }

        public int Radius
        {
            get
            {
                return coefficient.Length;
            }
        }

        public double this[int x]
        {
            get
            {
                x = Math.Abs(x);
                return x >= Radius ? 0 : coefficient[x];
            }
        }


        public static double Calculate(double sigma, int x)
        {
            x = Math.Abs(x);
            double result = Math.Exp(-(x * x) / (2 * sigma * sigma)) / (sigma * Math.Sqrt(2 * Math.PI));
            return result;
        }
    }
}
