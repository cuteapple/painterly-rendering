using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrushPainting
{
    public static class BitmapExtension
    {
        public static void ClearColor(this Bitmap bmp, Color color)
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(color);
            }
        }
    }
}
