using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTask
{
    class Item
    {
        public int Key { get; private set; }

        public int Value { get; private set; }

        public Item(int key, int value)
        {
            Key = key;
            Value = value;
        }
    }
}
