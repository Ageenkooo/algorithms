using System;

namespace GeneticAlg
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = new int[]{ 1, 1, 2, 0, 1, 0, 0, 0, 0, 1, 2, 1, 0, 0, 2, 1, 2, 2, 1, 0, 1, 2, 2, 2, 1, 40 };
            Genetic generation = new Genetic(arr);
            Console.WriteLine(generation.Solve());
            Console.ReadKey();
        }
    }
}
