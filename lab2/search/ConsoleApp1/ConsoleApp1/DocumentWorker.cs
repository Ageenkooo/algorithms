using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class DocumentWorker
    {
        public int[] RandomArrCreator(int n)
        {
            Random rnd = new Random();
            int[] arr = new int[n];
            for (int i = 0; i < n; i++)
            {
                int value = rnd.Next(0, 200);
                arr[i] = value;
            }
            return arr;
        }
    }
}
