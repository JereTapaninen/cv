using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gensim.Lib.DNA
{
    /*
     * In a female chromosome,
     * the first 4 genes indicate stamina,
     * and the last 4 genes indicate fertility
     */
    public class FemaleChromosome : Chromosome
    {
        public FemaleChromosome()
        {
            this.type = ChromosomeType.SexY;
            this.genome = new bool[8];
        }

        public FemaleChromosome(bool[] genome)
        {
            this.type = ChromosomeType.SexY;
            this.genome = genome;
        }

        public override Chromosome[] Crossover(Chromosome other)
        {
            var pivot = (int)(other.Genome.Length / 2) - 1;

            var child11 = new bool[4];
            var child12 = new bool[4];
            var child21 = new bool[4];
            var child22 = new bool[4];

            Split(Genome, pivot, out child11, out child12);
            Split(other.Genome, pivot, out child21, out child22);

            if (StaticRandom.Instance.Next(0, 2) == 0)
            {
                var child11list = child11.ToList();
                child11list.AddRange(child22);
                var child21list = child21.ToList();
                child21list.AddRange(child12);

                return new Chromosome[] { new FemaleChromosome(child11list.ToArray()), new FemaleChromosome(child21list.ToArray()) };
            }
            else
            {
                var child12list = child12.ToList();
                child12list.AddRange(child21);
                var child22list = child22.ToList();
                child22list.AddRange(child11);

                return new Chromosome[] { new FemaleChromosome(child12list.ToArray()), new FemaleChromosome(child22list.ToArray()) };
            }
        }

        /*public override void Mutate(double chance)
        {
            throw new NotImplementedException();
        }*/
    }
}
