using System;
using System.Linq;

namespace package
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Package pack = new Package();
            double[] items = new double[]
            {
                0.1, 0.3, 6*1.0/7, 8*1.0/109, 0.5, 0.7, 0.456, 1*1.0/3, 0.0001
            };
            Sorter.InsertionSort(items, 0, items.Count() - 1);

            foreach (var el in items)
            {
                Console.WriteLine("----------" + Array.IndexOf(items, el) + "----------");
                pack.AddItem(el);
                pack.ShowPacksLoad();
            }
            Menu.ShowMenu();
            
            Console.ReadKey();
        }
    }
}
