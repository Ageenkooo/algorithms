using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    class Program
    {
        static void Main(string[] args)
        {
            Tree btr = new Tree();
            Random rnd = new Random();
            for (int i = 0; i < 20; i++)
            {
                btr.Add(rnd.Next(0, 20));
            }

            TreePrinter.Print(btr.Root);
            btr._root = btr.RotateLeft(btr.Root);
            TreePrinter.Print(btr.Root);

            Console.ReadLine();
        }
    }
}
