using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Search
    {
        public int InterpolationSearch(int item, int[] array)
        {
            int low = 0;
            int high = array.Length - 1;
            int mid;

            while (array[low] < item && array[high] >= item)
            {
                mid = low + ((item - array[low]) * (high - low)) / (array[high] - array[low]);

                if (array[mid] < item)
                    low = mid + 1;
                else if (array[mid] > item)
                    high = mid - 1;
                else
                    return mid;
            }

            if (array[low] == item)
                return low;
            else
                return -1; 
        }

        public int BinarySearch(int item, int[] array)
        {
            int min = 0;
            int N = array.Length;
            int max = N - 1;
            do
            {
                int mid = (min + max) / 2;
                if (item > array[mid])
                    min = mid + 1;
                else
                    max = mid - 1;
                if (array[mid] == item)
                    return mid;
            } while (min <= max);
            return -1;
        }
    }
}
