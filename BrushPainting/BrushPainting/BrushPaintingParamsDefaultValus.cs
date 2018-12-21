using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrushPainting
{
    public static class BrushPaintingParamsDefaultValus
    {
        public static BrushPaintingParams Default
        {
            get
            {
                BrushPaintingParams param = new BrushPaintingParams();
                return param;
            }
        }
        public static BrushPaintingParams Impressionist
        {
            get
            {
                BrushPaintingParams param = new BrushPaintingParams();
                param.Threshold = 100;
                param.BrushRadius = new int[] { 8, 4, 2 };
                param.CurvatureFilter = 1;
                param.BlurFactor = 0.5;
                param.Opacity = 1;
                param.GridFactor = 1;
                param.MinimumStrokeLength = 4;
                param.MaximumStrokeLengths = 16;
                return param;
            }
        }

        public static BrushPaintingParams Expressionist
        {
            get
            {
                BrushPaintingParams param = new BrushPaintingParams();
                param.Threshold = 50;
                param.BrushRadius = new int[] { 8, 4, 2 };
                param.CurvatureFilter = .25;
                param.BlurFactor = .5;
                param.Opacity = .7;
                param.GridFactor = 1;
                param.MinimumStrokeLength = 10;
                param.MaximumStrokeLengths = 16;
                param.ColorJitter = new HSVColorJitter(0, 0, .5);
                //jv=5
                return param;
            }
        }

        public static BrushPaintingParams ColoristWash
        {
            get
            {
                BrushPaintingParams param = new BrushPaintingParams();
                param.Threshold = 200;
                param.BrushRadius = new int[] { 8, 4, 2 };
                param.CurvatureFilter = 1;
                param.BlurFactor = .5;
                param.Opacity = .5;
                param.GridFactor = 1;
                param.MinimumStrokeLength = 4;
                param.MaximumStrokeLengths = 16;
                param.ColorJitter = new RGBColorJitter(.3, .3, .3);
                //j(r,g,b)=.3
                return param;
            }
        }

        public static BrushPaintingParams Pointillist
        {
            get
            {
                BrushPaintingParams param = new BrushPaintingParams();
                param.Threshold = 100;
                param.BrushRadius = new int[] { 4, 2 };
                param.CurvatureFilter = 1;
                param.BlurFactor = .5;
                param.Opacity = 1;
                param.GridFactor = .5;
                param.MinimumStrokeLength = 0;
                param.MaximumStrokeLengths = 0;
                param.ColorJitter = new HSVColorJitter(.3, 0, 0);
                return param;
            }
        }


        public static BrushPaintingParams ExpressionistEx
        {
            get
            {
                BrushPaintingParams param = Expressionist;
                param.ColorJitter = new HSVColorJitter(.1, .1, .5);
                return param;
            }
        }

        public static BrushPaintingParams PointillistEx
        {
            get
            {
                var param = Pointillist;
                param.ColorJitter = new HSVColorJitter(.03, 0, 0);
                return param;
            }
        }


    }
}

