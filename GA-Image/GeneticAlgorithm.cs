using FIMG;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA_Image
{
    class GeneticAlgorithm
    {
        Random rnd;
        FastImage src;
        List<Candidate> candidates;
        int maxGens;
        int keepEvery;
        int topn;
        int mutN;
        int numCands, numGenes;
        int min, max;

        public GeneticAlgorithm(Bitmap sourceImage, int keepEvery, int maxGens, double mutRate,
            int topn, int numCands, int numGenes, int min, int max)
        {
            rnd = new Random();
            src = new FastImage(sourceImage);
            this.keepEvery = keepEvery;
            this.maxGens = maxGens;
            this.mutN = (int)(1 / mutRate);
            this.topn = topn;
            this.numCands = numCands;
            this.numGenes = numGenes;
            this.min = min;
            this.max = max;

            candidates = new List<Candidate>();
            for (int i = 0; i < numCands; i++)
            {
                Candidate n = new Candidate(src, numGenes, min, max);
                candidates.Add(n);
            }
        }

        public List<Bitmap> Run()
        {
            List<Bitmap> ret = new List<Bitmap>();

            for (int gen = 0; gen < maxGens; gen++)
            {
                Parallel.ForEach(candidates, (cand) =>
                {
                    cand.TestFitness(src);
                });
                candidates = candidates.OrderBy(x => x.error).ToList();
                Console.WriteLine(candidates.First().error);
                if (gen % keepEvery == 0)
                    ret.Add(candidates.First().f.toBitmap());
                Crossover();
                Mutate();
                //Console.WriteLine("Gen:" + gen);
            }

            return ret;
        }

        private void Mutate()
        {
            for (int i = 0; i < numCands; i++)
            {
                for (int j = 0; j < numGenes; j++)
                {
                    if (rnd.Next(0, mutN) == 0)
                        candidates[i].genes[j] = new Gene(src, 0, 0, src.width, src.height, min, max);
                }
            }
        }

        private void Crossover()
        {
            List<Candidate> best = candidates.Take(topn).Select(x => x.DeepCopy()).ToList();
            for (int i = 0; i < numCands; i++)
            {
                Candidate p1 = best[rnd.Next(0, best.Count)];
                Candidate p2 = best[rnd.Next(0, best.Count)];
                for (int j = 0; j < numGenes; j++)
                {
                        candidates[i].genes[j] = 
                            new[]{ p1.genes[j].Clone(), p2.genes[j].Clone()}[rnd.Next(0,2)];
                }
            }
        }
    }
}
