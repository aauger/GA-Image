using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FIMG
{
    public class FastImage
    {
        SimpleColor[,] pixels;
        public int width { get; }
        public int height { get; }

        public FastImage(Image a)
        {
            pixels = new SimpleColor[a.Width, a.Height];
            Bitmap b = (Bitmap)a;

            width = b.Width;
            height = b.Height;

            for (int x = 0; x < b.Width; x++)
            {
                for (int y = 0; y < b.Height; y++)
                {
                    Color c = b.GetPixel(x, y);
                    pixels[x, y] = new SimpleColor(c.R, c.G, c.B, ImageGenerationAlgo.Utils.Map(c.A, 0, 255, 0, 1));
                }
            }
        }

        public FastImage(FastImage c)
        {
            pixels = new SimpleColor[c.width, c.height];

            width = c.width;
            height = c.height;

            Parallel.For(0, width, x =>
            {
                Parallel.For(0, height, y =>
                {
                    SimpleColor color = c.GetPixel(x, y);
                    pixels[x, y] = new SimpleColor(color.GetR(), 
                        color.GetG(), 
                        color.GetB(), 
                        color.GetA());
                });
            });
        }

        public FastImage(int w, int h)
        {
            pixels = new SimpleColor[w, h];
            width = w;
            height = h;

            Parallel.For(0, w, x =>
            {
                Parallel.For(0, h, y =>
                {
                    pixels[x, y] = new SimpleColor(0, 0, 0, 0);
                });
            });
        }

        public void Clear()
        {
            Parallel.For(0, width, x =>
            {
                Parallel.For(0, height, y =>
                {
                    pixels[x, y].Set(0, 0, 0, 0);
                });
            });
        }

        public Bitmap toBitmap()
        {
            Bitmap ret = new Bitmap(width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    ret.SetPixel(x, y, pixels[x, y].ToColor());
                }
            }

            return ret;
        }

        public SimpleColor GetPixel(int x, int y)
        {
            return pixels[x, y];
        }

        public void DrawLine(int x, int y, int x2, int y2, SimpleColor c)
        {
            int w = x2 - x;
            int h = y2 - y;
            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
            if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
            if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
            int longest = Math.Abs(w);
            int shortest = Math.Abs(h);
            if (!(longest > shortest))
            {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                dx2 = 0;
            }
            int numerator = longest >> 1;
            for (int i = 0; i <= longest; i++)
            {
                if (x >= 0 && x < width && y >= 0 && y < height)
                    SetPixel(x, y, c);
                numerator += shortest;
                if (!(numerator < longest))
                {
                    numerator -= longest;
                    x += dx1;
                    y += dy1;
                }
                else
                {
                    x += dx2;
                    y += dy2;
                }
            }
        }


        public SimpleColor SetPixel(int x, int y, SimpleColor c)
        {
            pixels[x, y].Set(c.GetR(), c.GetG(), c.GetB(), c.GetA());
            return pixels[x, y];
        }

        public void SetPixel(int x, int y, Color c)
        {
            pixels[x, y] = SimpleColor.FromColor(c);
        }
    }
}
