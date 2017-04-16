using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GA_Image
{
    class Program
    {
        static void Main(string[] args)
        {
            Image input = Image.FromFile(args[0]);
            GeneticAlgorithm ga = new GeneticAlgorithm((Bitmap)input, 100, 500, .004, 15, 300, 500, 5, 30);
            List<Bitmap> results = ga.Run();

            for (int i = 0; i < results.Count; i++)
            {
                results[i].Save(i + ".png", System.Drawing.Imaging.ImageFormat.Png);
            }
        }
    }
}
