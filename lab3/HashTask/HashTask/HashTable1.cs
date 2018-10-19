using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTask
{
    class HashTable1
    {
        public static double[] indivConstants =
       {
            0.5471367,
            0.65123785541,
            0.1564865152,
            0.875645235
        };

        private readonly int _maxSize = 1000;

        private Dictionary<int, List<Item>> _items = null;

        public IReadOnlyCollection<KeyValuePair<int, List<Item>>> Items => _items?.ToList()?.AsReadOnly();

        public HashTable1()
        {
            _items = new Dictionary<int, List<Item>>(_maxSize);
        }

        public void Insert(int key, int value)
        {

            var item = new Item(key, value);

            var hash = GetHash(item.Key);

            List<Item> hashTableItem = null;
            if (_items.ContainsKey(hash))
            {
                hashTableItem = _items[hash];

                var oldElementWithKey = hashTableItem.SingleOrDefault(i => i.Key == item.Key);
                if (oldElementWithKey != null)
                {
                    throw new ArgumentException($"Хеш-таблица уже содержит элемент с ключом {key}. Ключ должен быть уникален.", nameof(key));
                }

                _items[hash].Add(item);
            }
            else
            {
                hashTableItem = new List<Item> { item };

                _items.Add(hash, hashTableItem);
            }
        }

        public void Delete(int key)
        {
            var hash = GetHash(key);

            if (!_items.ContainsKey(hash))
            {
                return;
            }

            var hashTableItem = _items[hash];

            var item = hashTableItem.SingleOrDefault(i => i.Key == key);

            if (item != null)
            {
                hashTableItem.Remove(item);
            }
        }

        public int Search(int key)
        {

            var hash = GetHash(key);

            if (!_items.ContainsKey(hash))
            {
                return -1;
            }

            var hashTableItem = _items[hash];

            if (hashTableItem != null)
            {
                var item = hashTableItem.SingleOrDefault(i => i.Key == key);

                if (item != null)
                {
                    return item.Value;
                }
            }

            return -1;
        }

        public int GetHash(int key)
        {
            double A = 0.6180339887499;
            int M = 1000;
            int IndividConst = 613;
            return (int)((key % IndividConst * A - (int)(key % IndividConst * A)) * M);
        }

        public int GetLargestLength()
        {
            int largestLength = 0;
            foreach (var keyValuePair in _items)
            {
                if (keyValuePair.Value.Count > largestLength)
                {
                    largestLength = keyValuePair.Value.Count;
                }
            }

            return largestLength;
        }
    }
}
