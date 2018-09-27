using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Sorter
    {
        public WholeData[] Merge_Sort(WholeData[] arr)
        {
            if (arr.Length == 1)
                return arr;
            int mid_point = arr.Length / 2;
            return Merge(Merge_Sort(arr.Take(mid_point).ToArray()), Merge_Sort(arr.Skip(mid_point).ToArray()));
        }

        public WholeData[] Merge(WholeData[] arr_1, WholeData[] arr_2)
        {
            int a = 0, b = 0;
            WholeData[] merged = new WholeData[arr_1.Length + arr_2.Length];
            for (int i = 0; i < arr_1.Length + arr_2.Length; i++)
            {
                if (b.CompareTo(arr_2.Length) < 0 && a.CompareTo(arr_1.Length) < 0)
                    if (arr_1[a].Age > arr_2[b].Age)
                        merged[i] = arr_2[b++];
                    else
                        merged[i] = arr_1[a++];
                else
                    if (b < arr_2.Length)
                    merged[i] = arr_2[b++];
                else
                    merged[i] = arr_1[a++];
            }
            return merged;
        }
    }
}
