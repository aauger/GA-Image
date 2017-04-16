using FIMG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageGenerationAlgo
{
    class Utils
    {

        public static int RGBClamp(int x)
        {
            return Clamp(x, 255);
        }

        public static int Clamp(int x, int max)
        {
            if (x < 0)
                return 0;
            else if (x > max)
                return max;
            else
                return x;
        }

        public static int AdvClamp(int x, int min, int max)
        {
            if (x < min)
                return min;
            if (x > max)
                return max;
            return x;
        }

        public static SimpleColor Blend(SimpleColor a, SimpleColor b)
        {
            int rval, gval, bval;
            double aval;

            rval = RGBClamp((int)(a.GetA() * (a.GetR() - b.GetR()) + b.GetR()));
            gval = RGBClamp((int)(a.GetA() * (a.GetG() - b.GetG()) + b.GetG()));
            bval = RGBClamp((int)(a.GetA() * (a.GetB() - b.GetB()) + b.GetB()));
            aval = a.GetA() + b.GetA() * (1 - a.GetA());

            return new SimpleColor(rval, gval, bval, aval);
        }

        public static double Map(double x, double olds, double olde, double news, double newe)
        {
            return (x - olds) / (olde - olds) * (newe - news) + news;
        }

        public static long Error(FastImage a, FastImage b)
        {
            long error = 0;

            for (int x = 0; x < a.width; x++)
            {
                for (int y = 0; y < a.height; y++)
                {
                    SimpleColor old = a.GetPixel(x, y);
                    SimpleColor srcCol = b.GetPixel(x, y);

                    error += (long)Math.Sqrt(
                    Math.Pow(old.GetR() - srcCol.GetR(), 2) +
                    Math.Pow(old.GetG() - srcCol.GetG(), 2) +
                    Math.Pow(old.GetB() - srcCol.GetB(), 2));
                }
            }

            return error;
        }
    }
}
