using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrushPainting
{
    public abstract class ColorJitter
    {
        abstract public Color Apply(Color color);
    }
    public class NoChangeColorJitter : ColorJitter
    {
        public override Color Apply(Color color)
        {
            return color;
        }
    }

    public class RGBColorJitter : ColorJitter
    {
        private byte JR, JG, JB;
        private Random random = new Random();
        //RGB: [0,1]
        public RGBColorJitter(double R, double G, double B)
            : this((byte)Math.Round(255 * R), (byte)Math.Round(255 * G), (byte)Math.Round(255 * B))
        {
        }

        //RGB: [0,255]
        public RGBColorJitter(byte R, byte G, byte B)
        {
            JR = R;
            JG = G;
            JB = B;
        }
        public override Color Apply(Color color)
        {
            return Color.FromArgb(
                color.A,
                ImageOperations.Clamp(color.R + (random.NextDouble() * 2 - 1) * JR),
                ImageOperations.Clamp(color.G + (random.NextDouble() * 2 - 1) * JG),
                ImageOperations.Clamp(color.B + (random.NextDouble() * 2 - 1) * JB)
                );
        }
    }
    public class HSVColorJitter : ColorJitter
    {
        private double JH, JS, JB;
        private Random random = new Random();
        //HSB: [0,1]
        public HSVColorJitter(double H, double S, double V)
        {
            JH = H;
            JS = S;
            JB = V;
        }
        public override Color Apply(Color color)
        {
            double h, s, v;
            ImageOperations.ColorToHSV(color, out h, out s, out v);

            double H = h / 360 + JH * (random.NextDouble() * 2 - 1);
            double S = s + JS * (random.NextDouble() * 2 - 1);
            double Bv = v + JB * (random.NextDouble() * 2 - 1);

            H *= 360;
            if (H > 360)
                H -= 360;
            if (H < 0)
                H += 360;
            if (S < 0)
                S = 0;
            if (S > 1)
                S = 1;
            if (Bv < 0)
                Bv = 0;
            if (Bv > 1)
                Bv = 1;

            return ImageOperations.HSVToColor(H, S, Bv);
        }

    }
}
