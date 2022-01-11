using System;

namespace SortBinaryTree_9
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = new int[20];
            BinaryTree binaryTree = new BinaryTree(arr[0]);
            FillArr(arr);
            Sorting(arr, binaryTree);

        }
        static void FillArr(int[] arr)
        {
            Random rnd = new Random();
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = rnd.Next(0, 100);
            }
        }
        static void Sorting(int[] arr, BinaryTree binaryTree)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Node node = binaryTree.root;
                while (true)
                {
                    if (arr[i] < node.Value)
                    {
                        if (node.LeftNode == null)
                        {
                            Node newNode = new Node(arr[i]);
                            node.LeftNode = newNode;
                            break;
                        }
                        else
                        {
                            node = node.LeftNode;
                        }
                    }
                    else
                    {
                        if (node.RightNode == null)
                        {
                            Node newNode = new Node(arr[i]);
                            node.RightNode = newNode;
                            break;
                        }
                        else
                        {
                            node = node.RightNode;
                        }
                    }
                }
            }
        }

    }

    class BinaryTree
    {
        public Node root;
        public BinaryTree(int value)
        {
            root = new Node(value);
        }

    }
    class Node
    {
        public Node LeftNode;
        public Node RightNode;

        int value;
        public int Value
        {
            get => value;
            set => this.value = value;
        }

        public Node(int value)
        {
            this.value = value;
        }

    }
}