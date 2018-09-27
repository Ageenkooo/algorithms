using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Sorter
    {
        public void Quicksort(int[] elements, int left, int right)
        {
            int i = left, j = right;
            int center = elements[(left + right) / 2];

            while (i <= j)
            {
                while (elements[i].CompareTo(center) < 0)
                {
                    i++;
                }

                while (elements[j].CompareTo(center) > 0)
                {
                    j--;
                }

                if (i <= j)
                {
                    int tmp = elements[i];
                    elements[i] = elements[j];
                    elements[j] = tmp;

                    i++;
                    j--;
                }
            }
            
            if (left < j)
            {
                Quicksort(elements, left, j);
            }
            if (i < right)
            {
                Quicksort(elements, i, right);
            }
        }

        public void InsertionSort(int[] array, int l, int r)
        {
            for (int i = l; i < r; i++)
            {
                for (int j = i + 1; j > l; j--)
                {
                    if (array[j - 1] > array[j])
                    {
                        int temp = array[j - 1];
                        array[j - 1] = array[j];
                        array[j] = temp;
                    }
                }
            }
        }

        public void HybridQuicksort(int[] elements, int left, int right, int n)
        {
            if(right - left <= n)
            {
                InsertionSort(elements, left, right);
            }
            else
            {
                int i = left, j = right;
                int center = elements[(left + right) / 2];

                while (i <= j)
                {
                    while (elements[i].CompareTo(center) < 0)
                    {
                        i++;
                    }

                    while (elements[j].CompareTo(center) > 0)
                    {
                        j--;
                    }

                    if (i <= j)
                    {
                        int tmp = elements[i];
                        elements[i] = elements[j];
                        elements[j] = tmp;

                        i++;
                        j--;
                    }
                }

                if (left < j)
                {
                    HybridQuicksort(elements, left, j, n);
                }
                if (i < right)
                {
                    HybridQuicksort(elements, i, right, n);
                }
            }
        }
    }
}
