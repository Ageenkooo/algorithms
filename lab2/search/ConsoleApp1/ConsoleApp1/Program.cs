using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            DocumentWorker dw = new DocumentWorker();
            Sorter sort = new Sorter();

            Random rnd = new Random();
            Search searcher = new Search();
            string output = " ";
                for (int i = 100; i >= 2; i--)
                {
                    File.WriteAllText($@"D:\c#\ConsoleApp1\numbers1.txt", string.Join(" ", dw.RandomArrCreator(i)));
                    int[] arr1 = File.ReadAllText($@"D:\c#\ConsoleApp1\numbers1.txt").Split(' ').Select(y => int.Parse(y)).ToArray();
                    
                    sort.Quicksort(arr1, 0, arr1.Length - 1);
                    
                    Stopwatch sw = Stopwatch.StartNew();
                    searcher.InterpolationSearch(arr1[1], arr1);
                    sw.Stop();

                    Console.WriteLine("Time taken: {0}ms", sw.Elapsed.TotalMilliseconds);

                    Stopwatch sw2 = Stopwatch.StartNew();
                    searcher.BinarySearch(arr1[1], arr1);
                    sw2.Stop();

                    Console.WriteLine("Time taken: {0}ms", sw2.Elapsed.TotalMilliseconds);

                    if (sw.Elapsed.TotalMilliseconds < sw2.Elapsed.TotalMilliseconds)
                    {
                        output = output + " " + i ;
                    }
                    
                }
            File.WriteAllText($@"D:\c#\ConsoleApp1\result.txt", output);
            Console.WriteLine("End");
            Console.ReadLine();
        }
    }
}
