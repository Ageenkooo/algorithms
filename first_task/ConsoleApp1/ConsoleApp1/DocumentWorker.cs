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
        public int[] RandomArrCreator()
        {
            Random rnd = new Random();
            int[] arr = new int[100000];
            for (int i = 0; i < 100000; i++)
            {
                int value = rnd.Next(0, 100000);
                arr[i] = value;
            }
            return arr;
        }
    }
}
