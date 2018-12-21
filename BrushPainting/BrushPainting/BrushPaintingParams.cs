using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrushPainting
{
    //[Serializable]
    public class BrushPaintingParams : ICloneable
    {
        public int[] BrushRadius = { 8, 4, 2, 1 };
        public double BlurFactor = 1;
        public double CurvatureFilter = 1;
        public int Threshold = 50;
        public int MinimumStrokeLength = 0;
        public int MaximumStrokeLengths = 16;
        public double Opacity = 1;
        public double GridFactor = 1;

        public int ColorJitterType;
        public ColorJitter ColorJitter = new NoChangeColorJitter();

        public BrushPaintingParams Clone()
        {
            return (BrushPaintingParams)MemberwiseClone();
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }
    }


    //parameters not from paper
    //[Serializable]
    public class BrushPaintingParamsEx : ICloneable
    {
        public int ProcessingSize_Width = 640;
        public Color InitialColor = Color.FromArgb(0, 0, 0);
        public Random random = null;

        public BrushPaintingParamsEx Clone()
        {
            var mclone = (BrushPaintingParamsEx)MemberwiseClone();
            mclone.random = random == null ? null : random.Clone();
            return mclone;
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }
    }
}
