using FIMG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA_Image
{
    class Gene
    {
        static private Random _rnd = new Random();
        public int x, y;
        public int sz;
        public SimpleColor color;

        private Gene()
        {
        }

        public Gene(FastImage src, int minx, int miny, int maxx, int maxy, int minsz, int maxsz)
        {
            x = _rnd.Next(minx, maxx);
            y = _rnd.Next(miny, maxy);
            sz = _rnd.Next(minsz, maxsz);
            //color = new SimpleColor(_rnd.Next(0, 256), _rnd.Next(0, 256), _rnd.Next(0, 256));
            SimpleColor sc = src.GetPixel(_rnd.Next(0, src.width), _rnd.Next(0, src.height));
            color = new SimpleColor(sc.GetR(), sc.GetG(), sc.GetB());
        }

        public Gene Clone()
        {
            Gene g = new Gene();
            g.x = x;
            g.y = y;
            g.sz = sz;
            g.color = new SimpleColor(color.GetR(), color.GetG(), color.GetB());

            return g;
        }
    }
}
