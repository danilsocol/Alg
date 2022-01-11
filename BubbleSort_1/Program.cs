using System;

namespace BubbleSort_1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = new int[20];
            FillArr(arr);
            Sorting(arr);
            Output(arr);
        }

        static void FillArr(int[] arr)
        {
            Random rnd = new Random();
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = rnd.Next(0,100);
            }
        }

        static void Sorting(int[] arr)
        {
            int i = 0;

            while (true)
            {
                if (i == arr.Length - 1)
                    break;

                if (arr[i] > arr[i + 1])
                {
                    Swap(arr, i,i+1);
                    i = 0;
                    continue;
                }
                i++;
            }
        }

        static void Swap(int[] arr, int i, int j)
        {
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
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
