using System;
namespace GeneticAlg
{
    public class Individ
    {
        private Random random = new Random();
        public int U { get; set; }
        public int W { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int Diff { get; set; }
        
        public String Show()
        {
            return $"Diff = {Diff}({U}, {W}, {X}, {Y}, {Z})";
        }

        public Individ(int from, int to)
        {
            U = random.Next(from, to);
            W = random.Next(from, to);
            X = random.Next(from, to);
            Y = random.Next(from, to);
            Z = random.Next(from, to);
        }

        public Individ(int u, int w, int x, int y, int z)
        {
            U = u;
            W = w;
            X = x;
            Y = y;
            Z = z;
        }
    }
}