using System;

namespace HashTableOpen_12
{
    class Program
    {
        static void Main(string[] args)
        {
            MyDictionary myDictionary = new MyDictionary(100);
            Random rnd = new Random();
            for (int i = 0; i < 90; i++)
            {
                myDictionary.Add(rnd.Next());
            }
            Console.WriteLine();
        }
    }
}
