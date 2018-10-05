using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    public class Node
    {
        public int data;
        public Node right_child;
        public Node left_child;

        public Node(int data)
        {
            this.data = data;
        }
        public int GetData()
        {
            return data;
        }
    }
}
