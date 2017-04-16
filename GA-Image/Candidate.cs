using FIMG;
using ImageGenerationAlgo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA_Image
{
    class Candidate
    {
        public FastImage f { get; private set; }
        public List<Gene> genes { get; set; }
        public int error { get { return _error; } }
        private int _error;

        private Candidate()
        {

        }

        public Candidate(FastImage src, int gCount, int minSqSz, int maxSqSz)
        {
            f = new FastImage(src.width, src.height);
            genes = new List<Gene>();
            for (int i = 0; i < gCount; i++)
            {
                Gene g = new Gene(src, 0, 0, src.width, src.height, minSqSz, maxSqSz);
                genes.Add(g);
            }
        }

        public void TestFitness(FastImage src)
        {
            f.Clear();
            _error = 0;

            foreach (Gene gene in genes)
            {
                for (int x = gene.x; x < src.width && x < gene.x + gene.sz; x++)
                {
                    for (int y = gene.y; y < src.height && y < gene.y + gene.sz; y++)
                    {
                        f.SetPixel(x, y, gene.color);
                    }
                }
            }

            for (int x = 0; x < src.width; x++)
            {
                for (int y = 0; y < src.height; y++)
                {
                    _error += src.GetPixel(x, y).Difference(f.GetPixel(x, y));
                }
            }
        }

        public Candidate DeepCopy()
        {
            List<Gene> genesCopied = new List<Gene>();
            Candidate c = new Candidate();
            c._error = _error;
            c.f = new FastImage(f);
            foreach (Gene gene in genes)
            {
                genesCopied.Add(gene.Clone());
            }
            c.genes = genesCopied;
            return c;
        }
    }
}
