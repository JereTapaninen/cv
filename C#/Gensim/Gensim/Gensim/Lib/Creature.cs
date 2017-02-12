using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using Gensim.Lib.DNA;

namespace Gensim.Lib
{
    public class Creature
    {
        private Chromosome[] dna;
        public Chromosome[] DNA
        {
            get { return dna; }
        }

        private World world;
        public World World
        {
            get { return world; }
        }

        private Point currentTile;
        public Point CurrentTile
        {
            get { return this.currentTile; }
        }

        private double age;
        public double Age
        {
            get { return age; }
        }

        private int battlesWon;
        public int BattlesWon
        {
            get { return battlesWon; }
            set { battlesWon = value; }
        }

        private List<Creature> parents = new List<Creature>();
        public List<Creature> Parents
        {
            get { return parents; }
        }

        private List<Creature> offspring = new List<Creature>();
        public List<Creature> Offspring
        {
            get { return offspring; }
        }

        private double mutationchance = 0.05;

        public bool InBattle = false;

        public Gender Gender
        {
            get
            {
                if (DNA.Count(c => c is FemaleChromosome) > 1)
                    return Gender.Female;
                else
                    return Gender.Male;
            }
        }

        // overall fitness
        public double Fitness
        {
            get
            {
                return Math.Pow(Offspring.Count, 2) + BattlesWon + Age;
            }
        }

        public int Stamina
        {
            get
            {
                if (Gender == Gender.Male)
                {
                    return DNA[0].Genome.Take(4).Count(c => c == true);
                }
                else
                {
                    return DNA[0].Genome.Take(4).Count(c => c == true) +
                        DNA[1].Genome.Take(4).Count(c => c == true);
                }
            }
        }

        public int Fertility
        {
            get
            {
                if (Gender == Gender.Male)
                {
                    return DNA[0].Genome.Skip(4).Count(c => c == true);
                }
                else
                {
                    return DNA[0].Genome.Skip(4).Count(c => c == true) +
                        DNA[1].Genome.Skip(4).Count(c => c == true);
                }
            }
        }

        public int Intelligence
        {
            get
            {
                if (Gender == Gender.Female)
                    return 0;

                return DNA[1].Genome.Skip(4).Count(c => c == true);
            }
        }

        public int Strength
        {
            get
            {
                if (Gender == Gender.Female)
                    return 0;

                return DNA[1].Genome.Take(4).Count(c => c == true);
            }
        }

        private Brush MCreatureBrush = Brushes.Red;
        private Brush FCreatureBrush = Brushes.Blue;

        private Random Random = new Random();
        private object SyncLock = new object();

        public Creature(World currentWorld, Point position)
        {
            this.world = currentWorld;
            this.currentTile = position;

            var chromosomeList = new List<Chromosome>();

            chromosomeList.Add(new FemaleChromosome());

            if (StaticRandom.Instance.Next(0, 2) == 0)
                chromosomeList.Add(new FemaleChromosome());
            else
                chromosomeList.Add(new MaleChromosome());

            this.dna = chromosomeList.ToArray();
        }

        public Creature(World currentWorld, Point position, Chromosome[] dna)
        {
            this.world = currentWorld;
            this.currentTile = position;
            this.dna = dna;
        }

        private DateTime movementDateTime;
        public void Update(DateTime now)
        {
            if (movementDateTime == null)
                movementDateTime = now;

            if (now.Subtract(movementDateTime).TotalMilliseconds >= 250)
            {
                age++;

                DNA.ToList().ForEach(c => c.Mutate(mutationchance));

                var creaturesNear = World.CreaturesNear(CurrentTile);

                if (creaturesNear != null)
                {
                    foreach (var c in creaturesNear)
                    {
                        if (c != null)
                        {
                            if (c.Gender == this.Gender
                            && !parents.Contains(c) && !offspring.Contains(c)
                            && !c.InBattle)
                            {
                                // battle
                                Battle(c);
                            }
                            else
                            {
                                if (!c.InBattle && !parents.Contains(c) &&
                                    !offspring.Contains(c))
                                {
                                    // try to mate
                                    var result = c.Mate(this);

                                    if (result != null)
                                    {
                                        result.ToList().ForEach(cc => cc.AddParents(new Creature[] { this, c }));

                                        //Console.WriteLine("Mating successful! Fertility: " + (c.Fertility + Fertility));
                                        AddOffspring(result);
                                        c.AddOffspring(result);
                                        World.AddCreatures(result);
                                    }
                                }
                                //else
                                //    Console.WriteLine("Mating unsuccessful! Other: " + c.Fitness + ", me: " + Fitness);
                            }
                        }
                    }
                }

                MoveToRandomTile();

                movementDateTime = now;
            }
        }

        public void Draw(Graphics g)
        {
            g.FillRectangle((Gender == Gender.Male) ? MCreatureBrush
                : FCreatureBrush, new Rectangle(CurrentTile.X * 16, CurrentTile.Y * 16, 16, 16));
        }

        public void AddParents(Creature[] creatures)
        {
            if (creatures != null)
                this.parents.AddRange(creatures);
        }

        public void AddOffspring(Creature[] creatures)
        {
            this.offspring.AddRange(creatures);
        }

        public void Battle(Creature other)
        {
            other.InBattle = true;
            this.InBattle = true;

            // battle!
            if (other.Strength > Strength)
            {
                if (StaticRandom.Instance.Next(0, 2) == 0)
                {
                    // escape tactically
                    if (Intelligence > other.Intelligence)
                    {
                        MoveToRandomTile();
                    }
                    else
                    {
                        // flee
                        if (Stamina > other.Stamina)
                        {
                            MoveToRandomTile();
                        }
                        else
                        {
                            World.KillCreature(this);
                        }
                    }
                }
                else
                {
                    World.KillCreature(this);
                }
            }
            else
            {
                if (StaticRandom.Instance.Next(0, 2) == 0)
                {
                    if (other.Intelligence > Intelligence)
                    {
                        other.MoveToRandomTile();
                    }
                    else
                    {
                        if (other.Stamina > Stamina)
                        {
                            other.MoveToRandomTile();
                        }
                        else
                        {
                            World.KillCreature(other);
                        }
                    }
                }
                else
                {
                    World.KillCreature(other);
                }
            }

            other.InBattle = false;
            other.InBattle = false;
        }

        public void MoveToRandomTile()
        {
            Point proposedTile;

            do
            {
                var moveX = StaticRandom.Instance.Next(0, 3);
                var moveY = StaticRandom.Instance.Next(0, 3);

                proposedTile = CurrentTile;

                switch (moveX)
                {
                    case 2:
                        // move to the left;
                        proposedTile.X -= 1;
                        break;
                    case 1:
                        // move to the right
                        proposedTile.X += 1;
                        break;
                    case 0:
                    default:
                        // do nothing, don't move
                        break;
                }

                switch (moveY)
                {
                    case 2:
                        // move up;
                        proposedTile.Y -= 1;
                        break;
                    case 1:
                        // move down
                        proposedTile.Y += 1;
                        break;
                    case 0:
                    default:
                        // do nothing, don't move
                        break;
                }
            }
            while (World.OutOfBounds(proposedTile) || World.TileOccupied(proposedTile));

            currentTile = proposedTile;
        }

        public Creature[] Mate(Creature other)
        {
            if (other.Fitness < Fitness)
                return null;

            var dnalist = new List<Chromosome[]>();

            var randomAmount = StaticRandom.Instance.Next(1, ((Fertility + other.Fertility) / 2) + 1);

            if (randomAmount == 0)
                return null;

            for (var i = 0; i < DNA.Length; i++)
            {
                dnalist.Add(DNA[i].Crossover(other.DNA[i]));
            }

            var creatureList = new List<Creature>();

            for (var i = 0; i < randomAmount; i++)
            {
                var chromosomeList = new List<Chromosome>();

                for (var j = 0; j < dnalist.Count; j++)
                {
                    if (StaticRandom.Instance.Next(0, 2) == 0)
                        chromosomeList.Add(dnalist[j][0]);
                    else
                        chromosomeList.Add(dnalist[j][1]);
                }

                creatureList.Add(new Creature(World, CurrentTile, chromosomeList.ToArray()));
            }

            return creatureList.ToArray();
        }
    }
}
