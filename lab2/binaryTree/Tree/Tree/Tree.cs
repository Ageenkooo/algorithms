using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    class Tree
    {
        public Node _root;
        private int _count;
        private IComparer<int> _comparer = Comparer<int>.Default;


        public Tree()
        {
            _root = null;
            _count = 0;
        }

        public Node Search(int data)
        {
            Node node = Search(_root, data);
            return node;
        }

        private Node Search(Node node, int data)
        {
            if (node == null)
            {
                return null;
            }
            if (data < node.data)
            {
                node.left_child = Search(node.left_child, data);
            }
            else if (data > node.data)
            {
                node.right_child = Search(node.right_child, data);
            }
            return node;
        }

        public bool ContainsNode(int data)
        {
            return ContainsNodeRecursive(_root, data);
        }

        private bool ContainsNodeRecursive(Node node, int data)
        {
            if (node == null)
            {
                return false;
            }
            if (data == node.data)
            {
                return true;
            }
            return data < node.data
                ? ContainsNodeRecursive(node.left_child, data)
                : ContainsNodeRecursive(node.right_child, data);
        }

        private Node DeleteNode(Node root, Node deleteNode)
        {
            if (root == null)
            {
                return root;
            }
            if (deleteNode.data < root.data)
            {
                root.left_child = DeleteNode(root.left_child, deleteNode);
            }
            if (deleteNode.data > root.data)
            {
                root.right_child = DeleteNode(root.right_child, deleteNode);
            }
            if (deleteNode.data == root.data)
            {
               
                if (root.left_child == null && root.right_child == null)
                {
                    root = null;
                    return root;
                }
                
                else if (root.left_child == null)
                {
                    Node temp = root;
                    root = root.right_child;
                }
                else if (root.right_child == null)
                {
                    Node temp = root;
                    root = root.left_child;
                }
                else
                {
                    Node min = FindMinimal(root.right_child);
                    root.data = min.data;
                    root.right_child = DeleteNode(root.right_child, min);
                }
            }
            return root;
        }

        private Node FindMinimal(Node data)
        {
            if (data.left_child != null)
            {
                return FindMinimal(data.left_child);
            }
            return data;
        }

        public void DeleteNode(int data)
        {
            Node deleteNode = new Node(data);
            DeleteNode(_root, deleteNode);
        }

        public Node RotateRight(Node node)
        {
            var oldroot = node;
            var newroot = node.left_child;
            oldroot.left_child = newroot.right_child;
            newroot.right_child = oldroot;
            return newroot;
        }

        public Node RotateLeft(Node node)
        {
            var oldroot = node;
            var newroot = node.right_child;
            oldroot.right_child = newroot.left_child;
            newroot.left_child = oldroot;
            return newroot;
        }


        public void InsertInRoot(int data)
        {
            _root = InsertInRoot(data, _root);
        }

        private Node InsertInRoot(int data, Node root)
        {
            if (root == null)
                return new Node(data);
            if (data < root.data)
            {
                root.left_child = InsertInRoot(data, root.left_child);
                root = RotateRight(root);
            }
            else if (data > root.data)
            {
                root.right_child = InsertInRoot(data, root.right_child);
                root = RotateLeft(root);
            }
            return root;

        }

        public bool Add(int data)
        {
            if (_root == null)
            {
                _root = new Node(data);
                _count++;
                return true;
            }
            else
            {
                return Add_Sub(_root, data);
            }
        }

        private bool Add_Sub(Node node, int data)
        {
            if (_comparer.Compare(node.data, data) < 0)
            {
                if (node.right_child == null)
                {
                    node.right_child = new Node(data);
                    _count++;
                    return true;
                }
                else
                {
                    return Add_Sub(node.right_child, data);
                }
            }
            else if (_comparer.Compare(node.data, data) > 0)
            {
                if (node.left_child == null)
                {
                    node.left_child = new Node(data);
                    _count++;
                    return true;
                }
                else
                {
                    return Add_Sub(node.left_child, data);
                }
            }
            else
            {
                return false;
            }
        }

        public Node Root { get { return _root; } }
    }
}

