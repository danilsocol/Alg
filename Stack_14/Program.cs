using System;

namespace Stack_14
{
    class Program
    {
        static void Main(string[] args)
        {
            MyStack<int> stack = new MyStack<int>();

            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);
            stack.Push(5);

            stack.Pop();
            stack.Top();
            stack.isEmpty();
            stack.Print();
        }
    }

    class MyStack<T>
    {
        MyDoubleLinkedList<T> list = new MyDoubleLinkedList<T>();
        public void Push(T value) => list.Add(value);

        public T Pop()
        {
            T value = list.ReturnLast();
            list.RemoveLast();
            return value;
        }
        public T Top() => list.ReturnLast();
        public bool isEmpty() => list.Count == 0 ? true : false;

        public void Print() => list.Print();

    }

    class MyDoubleLinkedList<T>
    {
        Node<T> firtsNode;
        Node<T> lastNode;
        int count;
        public int Count
        {
            get
            {
                return count;
            }
            set
            {
                count = value;
            }
        }

        public T ReturnLast() => lastNode.Value;
        public T ReturnFirst() => firtsNode.Value;
        public void Add(T value)
        {
            Node<T> node = new Node<T>(value);

            if (firtsNode == null && lastNode == null)
            {
                firtsNode = node;
            }
            else
            {
                node.nodeBack = lastNode;
                lastNode.nodeNext = node;
            }

            lastNode = node;
            count++;
        }
        public void Remove(T value)
        {
            Node<T> node = FindNode(value);

            Node<T> temp = node.nodeBack;
            temp.nodeNext = node.nodeNext;

            temp = node.nodeNext;
            temp.nodeBack = node.nodeBack;

            count--;
        }

        public void RemoveLast()
        {
            Node<T> node = lastNode.nodeBack;
            node.nodeNext = null;
            lastNode = node;

            count--;
        }
        public void RemoveFirst()
        {
            Node<T> node = firtsNode.nodeNext;
            node.nodeBack = null;
            firtsNode = node;

            count--;
        }
        Node<T> FindNode(T value)
        {
            Node<T> node = firtsNode;
            
            for (int i = 0; i < count; i++)
            {
                if (node.Value.Equals(value))
                {
                    return node;
                }
                node = node.nodeNext;
            }

            return null;
        }

        public void Print()
        {
            Node<T> node = firtsNode;

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(node.Value);
                node = node.nodeNext;
            }
        }
    }

    class Node<T>
    {
        T value;
        public T Value {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
            }
        }
        public Node<T> nodeBack;
        public Node<T> nodeNext;

        public Node(T value)
        {
            this.value = value;
        }
    }
}
