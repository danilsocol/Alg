using System;

namespace InsertSort_2
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = new int[20];
            FillArr(arr);
            Sort(arr);
            Output(arr);
        }

        static void FillArr(int[] arr)
        {
            Random rnd = new Random();
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = rnd.Next(0, 100);
            }
        }
        static void Sort(int[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                int temp = arr[i];
                int j = i;
                while (j > 0 && temp < arr[j - 1])
                {
                    arr[j] = arr[j - 1];
                    j--;
                }
                arr[j] = temp;
            }
        }

        static void Output(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write($"{arr[i]} ");
            }
            Console.ReadLine();
        }

    }
}
