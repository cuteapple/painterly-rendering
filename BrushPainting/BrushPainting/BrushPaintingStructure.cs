using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrushPainting
{
    public struct Stroke
    {
        public Color Color;
        public List<Point> Points;
        public int Radius;
    }
}
