using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace package
{
    static class Sorter
    {
        public static void InsertionSort(double[] array, int l, int r)
        {
            for (int i = l; i < r; i++)
            {
                for (int j = i + 1; j > l; j--)
                {
                    if (array[j - 1] < array[j])
                    {
                        double temp = array[j - 1];
                        array[j - 1] = array[j];
                        array[j] = temp;
                    }
                }
            }
        }
    }
}
