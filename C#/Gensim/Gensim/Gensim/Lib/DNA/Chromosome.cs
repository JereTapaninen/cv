using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gensim.Lib.DNA
{
    public abstract class Chromosome
    {
        protected ChromosomeType type;
        public ChromosomeType Type
        {
            get { return type; }
        }

        protected bool[] genome; // genome is always a 8 number long binary system
        public bool[] Genome
        {
            get { return genome; }
        }

        protected double fitness;
        public double Fitness
        {
            get { return fitness; }
        }

        protected Creature owner;
        public Creature Owner
        {
            get { return owner; }
        }

        public void Mutate(double chance)
        {
            if (StaticRandom.Instance.NextDouble() > chance)
                return;

            var randomIndex = StaticRandom.Instance.Next(0, Genome.Length);
            genome[randomIndex] = !genome[randomIndex];
        }

        public abstract Chromosome[] Crossover(Chromosome other);

        //public abstract void CalculateFitness();

        protected void Split<T>(T[] array, int index, out T[] first, out T[] second)
        {
            first = array.Take(index).ToArray();
            second = array.Skip(index).ToArray();
        }

        protected void SplitMidPoint<T>(T[] array, out T[] first, out T[] second)
        {
            Split(array, array.Length / 2, out first, out second);
        }

    }
}
