﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

namespace FIMG
{
    public class SimpleColor
    {
        int R, G, B;
        double A;

        public SimpleColor(int R, int G, int B)
        {
            Debug.Assert(R >= 0 && R <= 255 &&
                         G >= 0 && G <= 255 &&
                         B >= 0 && B <= 255);
            this.R = R;
            this.G = G;
            this.B = B;
            this.A = 1;
        }

        public SimpleColor(int R, int G, int B, double A)
        {
            Debug.Assert(R >= 0 && R <= 255 &&
                         G >= 0 && G <= 255 &&
                         B >= 0 && B <= 255 &&
                         A >= 0 && A <= 255);
            this.R = R;
            this.G = G;
            this.B = B;
            this.A = A;
        }

        public void Set(int R, int G, int B)
        {
            Debug.Assert(R >= 0 && R <= 255 &&
                         G >= 0 && G <= 255 &&
                         B >= 0 && B <= 255);
            this.R = R;
            this.G = G;
            this.B = B;
        }

        private float Lerp(float firstFloat, float secondFloat, float by)
        {
            return firstFloat * by + secondFloat * (1 - by);
        }

        public static float Map(float x, float olds, float olde, float news, float newe)
        {
            return (x - olds) / (olde - olds) * (newe - news) + news;
        }

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

        public void Set(int R, int G, int B, double A)
        {
            Debug.Assert(R >= 0 && R <= 255 &&
                         G >= 0 && G <= 255 &&
                         B >= 0 && B <= 255 &&
                         A >= 0 && A <= 1);
            this.R = RGBClamp((int)(A * (R - this.R) + this.R));
            this.G = RGBClamp((int)(A * (G - this.G) + this.G));
            this.B = RGBClamp((int)(A * (B - this.B) + this.B));
            this.A = A + this.A * (1 - A);
        }

        public void SetR(int R)
        {
            Debug.Assert(R >= 0 && R <= 255);
            this.R = R;
        }

        public void SetG(int G)
        {
            Debug.Assert(G >= 0 && G <= 255);
            this.G = G;
        }

        public void SetB(int B)
        {
            Debug.Assert(B >= 0 && B <= 255);
            this.B = B;
        }

        public void SetA(int A)
        {
            Debug.Assert(A >= 0 && A <= 1);
            this.A = A;
        }

        public int GetR()
        {
            return R;
        }

        public int GetG()
        {
            return G;
        }

        public int GetB()
        {
            return B;
        }

        public double GetA()
        {
            return A;
        }

        public Color ToColor()
        {
            return Color.FromArgb(RGBClamp((int)Map((float)A, 0, 1, 0, 255)) , R, G, B);
        }

        public static SimpleColor FromColor(Color c)
        {
            return new SimpleColor(c.R, c.G, c.B, c.A);
        }

        public int Difference(SimpleColor b)
        {
            return (int)Math.Sqrt(
                (R - b.R) * (R - b.R) +
                (G - b.G) * (G - b.G) +
                (B - b.B) * (B - b.B)
                );
        }
    }


}
