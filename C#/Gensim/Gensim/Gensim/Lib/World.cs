using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Gensim.Lib
{
    public class World
    {
        private Creature[] creatures;
        public Creature[] Creatures
        {
            get { return creatures; }
        }

        private Size worldSize;
        public Size WorldSize
        {
            get { return worldSize; }
        }

        private Brush WorldBackground = Brushes.Black;
        private Brush FPSBrush = Brushes.Yellow;
        private Pen TilePen = Pens.LightGray;

        private Random Random = new Random();
        private object SyncLock = new object();

        public World(Size size)
        {
            this.worldSize = new Size((size.Width / 16) * 16, (size.Height / 16) * 16);

            this.PopulateWorld(30);
        }

        public void Update(DateTime now)
        {
            Creatures.ToList().ForEach(c => c.Update(now));
        }

        public void Draw(Graphics g)
        {
            CalculateFramesPerSecond();

            // Clear the canvas
            g.FillRectangle(WorldBackground, new Rectangle(new Point(0, 0), WorldSize));

            Creatures.ToList().ForEach(c => c.Draw(g));

            // Draw tiles 
            for (var x = 0; x < WorldSize.Width / 16; x++)
            {
                for (var y = 0; y < WorldSize.Height / 16; y++)
                {
                    g.DrawRectangle(TilePen, new Rectangle(x * 16, y * 16, 16, 16));
                }
            }

            // Draw FPS
            var fpsString = "FPS: " + currentFrameRate;
            var fpsFont = new Font("Tahoma", 20f, FontStyle.Bold);
            var measureStringWidth = g.MeasureString(fpsString, fpsFont).Width;

            g.DrawString(fpsString, fpsFont, FPSBrush, new PointF(WorldSize.Width - measureStringWidth - 4, 4));
        }

        // to be used when mating
        public void AddCreatures(Creature[] creatures)
        {
            var creatureList = Creatures.ToList();
            creatureList.AddRange(creatures);
            this.creatures = creatureList.ToArray();
        }

        public Creature CreatureAtTile(Point tile)
        {
            return Creatures.FirstOrDefault(c => c.CurrentTile == tile);
        }

        public Creature[] CreaturesNear(Point tile)
        {
            var creatureList = new List<Creature>();

            if (CreatureAtTile(new Point(tile.X - 1, tile.Y - 1)) != null)
                creatureList.Add(CreatureAtTile(new Point(tile.X - 1, tile.Y - 1)));

            if (CreatureAtTile(new Point(tile.X, tile.Y - 1)) != null)
                creatureList.Add(CreatureAtTile(new Point(tile.X, tile.Y - 1)));

            if (CreatureAtTile(new Point(tile.X + 1, tile.Y - 1)) != null)
                creatureList.Add(CreatureAtTile(new Point(tile.X + 1, tile.Y - 1)));

            if (CreatureAtTile(new Point(tile.X + 1, tile.Y)) != null)
                creatureList.Add(CreatureAtTile(new Point(tile.X + 1, tile.Y)));

            if (CreatureAtTile(new Point(tile.X + 1, tile.Y + 1)) != null)
                creatureList.Add(CreatureAtTile(new Point(tile.X + 1, tile.Y + 1)));

            if (CreatureAtTile(new Point(tile.X, tile.Y + 1)) != null)
                creatureList.Add(CreatureAtTile(new Point(tile.X, tile.Y + 1)));

            if (CreatureAtTile(new Point(tile.X - 1, tile.Y + 1)) != null)
                creatureList.Add(CreatureAtTile(new Point(tile.X - 1, tile.Y + 1)));

            if (CreatureAtTile(new Point(tile.X - 1, tile.Y)) != null)
                creatureList.Add(CreatureAtTile(new Point(tile.X - 1, tile.Y)));

            return creatureList.ToArray();
        }

        public bool OutOfBounds(Point tile)
        {
            return tile.X * 16 >= WorldSize.Width
                || tile.Y * 16 >= WorldSize.Height
                || tile.X * 16 < 0 || tile.Y * 16 < 0;
        }

        public bool TileOccupied(Point tile)
        {
            return Creatures.Where(c => c.CurrentTile == tile).Count() > 0;
        }

        public void KillCreature(Creature c)
        {
            var creatureList = Creatures.ToList();

            creatureList.Remove(c);

            creatures = creatureList.ToArray();
        }

        private void PopulateWorld(int amount)
        {
            var alreadyPopulatedTiles = new List<Point>();
            var creatureList = new List<Creature>();

            for (var i = 0; i < amount; i++)
            {
                lock (SyncLock)
                {
                    var randTile = Point.Empty;

                    do
                    {
                        randTile = new Point(Random.Next(0, (worldSize.Width / 16) + 1),
                            Random.Next(0, (worldSize.Height / 16) + 1));
                    }
                    while (alreadyPopulatedTiles.Contains(randTile));

                    creatureList.Add(new Creature(this, randTile));
                }
            }

            this.creatures = creatureList.ToArray();
        }

        private double lastFramerateUpdate;
        private int callCount;
        private double currentFrameRate;
        private void CalculateFramesPerSecond()
        {
            callCount++;

            if (Environment.TickCount - lastFramerateUpdate >= 1000)
            {
                lastFramerateUpdate = Environment.TickCount;
                currentFrameRate = callCount;
                callCount = 0;
            }
        }
    }
}
