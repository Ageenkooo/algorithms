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
            for(int i=0; i<50; i++)
            {
                File.WriteAllText($@"D:\c#\ConsoleApp1\numbers{i}.txt", string.Join(" ", dw.RandomArrCreator()));
            }

            string output = " i - HS QS \r\n";
            for (int i=7; i<200; i++)
            {
                int resH = 0;
                int resQ = 0;
                for (int j = 0; j < 50; j++)
                {
                    int[] arr1 = File.ReadAllText($@"D:\c#\ConsoleApp1\numbers{j}.txt").Split(' ').Select(y => int.Parse(y)).ToArray();
                    int[] arr2 = File.ReadAllText($@"D:\c#\ConsoleApp1\numbers{j}.txt").Split(' ').Select(y => int.Parse(y)).ToArray();

                    Sorter sort = new Sorter();

                    Stopwatch stopwatch = Stopwatch.StartNew();
                    sort.HybridQuicksort(arr1, 0, arr1.Length - 1, i);
                    stopwatch.Stop();

                    Stopwatch stopwatch2 = Stopwatch.StartNew();
                    sort.Quicksort(arr2, 0, arr2.Length - 1);
                    stopwatch2.Stop();

                    resH += (int)stopwatch.ElapsedMilliseconds;
                    resQ += (int)stopwatch2.ElapsedMilliseconds;
                }
                resH /= 50;
                resQ /= 50;
                if (resH <= resQ)
                {
                    output = output + " " + i + " - " + resH + " " + resQ + " \r\n";
                }
            }
            File.WriteAllText(@"D:\c#\ConsoleApp1\sortedNumbers.txt", output);
        }
    }
}
