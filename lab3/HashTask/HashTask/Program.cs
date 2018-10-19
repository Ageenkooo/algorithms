using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTask
{
    class Program
    {
        static void Main(string[] args)
        {
            DocumentWorker dw = new DocumentWorker();

            File.WriteAllText($@".\numbersForHash.txt", string.Join(" ", dw.RandomCreator(1000)));
            int[] arr1 = File.ReadAllText($@".\numbersForHash.txt").Split(' ').Select(y => int.Parse(y)).ToArray();
            var hashTable1 = new HashTable1();

            for(int i=0; i< arr1.Length; i++)
            {
                hashTable1.Insert(arr1[i], arr1[i]*2);
            }

            Console.WriteLine(hashTable1.GetLargestLength());

            ShowHashTable(hashTable1, "Created hashtable.");
            Console.ReadLine();

            hashTable1.Delete(356);
            ShowHashTable(hashTable1, "Delete item from hashtable.");
            Console.ReadLine();

            Console.WriteLine("Search");
            var text = hashTable1.Search(45);
            Console.WriteLine(text);
            Console.ReadLine();

            File.WriteAllText($@".\numbersForHash2.txt", string.Join(" ", dw.RandomCreator(1024)));
            int[] arr2 = File.ReadAllText($@".\numbersForHash2.txt").Split(' ').Select(y => int.Parse(y)).ToArray();
            HashTable2 hashTable2 = new HashTable2(1024);

            for (int i = 0; i < arr2.Length; i++)
            {
                hashTable2.Insert(arr2[i], arr2[i] * 2);
            }
            hashTable2.ShowTable();
        }
        private static void ShowHashTable(HashTable1 hashTable, string title)
        {
            if (hashTable == null)
            {
                throw new ArgumentNullException(nameof(hashTable));
            }

            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException(nameof(title));
            }

            Console.WriteLine(title);
            foreach (var item in hashTable.Items)
            {
                Console.WriteLine(item.Key);

                foreach (var value in item.Value)
                {
                    Console.WriteLine($"\t{value.Key} - {value.Value}");
                }
            }
            Console.WriteLine();
        }
    }
}
