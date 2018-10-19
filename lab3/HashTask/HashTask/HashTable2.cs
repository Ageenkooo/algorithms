using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTask
{
    public class HashTable2
    {
        public int N = 1024;
        private Item[] table;

        public HashTable2()
        {
            table = new Item[N];
            Console.WriteLine(table.ToString());
        }

        public HashTable2(int size)
        {
            table = new Item[size];
            Console.WriteLine(table.ToString());
        }

        public void ShowTable()
        {
            foreach (var item in table)
            {
                if (item != null)
                    Console.WriteLine($"{GetHash(item.Value)} {item.Key} {item.Key}");
                else
                {
                    Console.WriteLine("null");
                }
            }
        }

        public void Insert(int key, int value)
        {
            int h = GetHash(key);
            int i = 1;
            try
            {

                if (table[h] == null)
                {
                    table[h] = new Item(key, value);
                }
                else if (table[h].Value != value)
                {
                    int newHash = (h + i) % N;
                    while (table[newHash] == null || table[newHash].Value != value)
                    {
                        newHash = (h + i * i) % N;
                        i += 1;
                        if (table[newHash] == null || table[newHash].Value != value)
                            table[newHash] = new Item(key, value);
                    }
                }
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine($"Exception occured: {e.Message}");
            }
        }

        public void Delete(int key)
        {
            int h = GetHash(key);
            try
            {
                if (table[h].Key == key)
                {
                    table[h] = null;
                }
                for (int i = h + 1; i != h; i = (i + 1) % table.Length)
                {
                    if (table[i].Value == key && table[i] != null)
                    {
                        table[i] = null;
                    }
                }
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine($"Exception: {e.Message}");
            }
        }

        public int Find(int key)
        {
            int hash = GetHash(key);
            int i = 1;

            if (table[hash] == null)
            {
                return -1;
            }
            else if (table[hash].Key != key)
            {
                int newHash = (hash + i) % N;


                while (table[newHash] == null && table[newHash].Key != key)
                {
                    newHash = (hash + i * i) % N;
                    i += 1;
                }
                if (table[newHash].Key == key)
                    return newHash;
                else
                    return -1;

            }
            else
                return hash;
        }

        private int GetHash(int key)
        {
            double A = 0.6180339887499;
            int M = 1000;
            int IndividConst = 613;
            return (int)((key % IndividConst * A - (int)(key % IndividConst * A)) * M);
        }
    }
}
