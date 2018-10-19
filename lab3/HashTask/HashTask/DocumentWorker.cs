using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTask
{
    class DocumentWorker
    {
        public int[] RandomCreator(int size)
        {
            Random rnd = new Random();
            int[] arr = new int[size];
            for (int i = 0; i < size; i++)
            {
                int value = rnd.Next(0, size+1);
                if (!arr.Contains(value))
                {
                    arr[i] = value;
                }
                else
                {
                    i--;
                }
            }
            return arr;
        }
    }
}
