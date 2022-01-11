using System;

namespace MixingSort_4
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
                arr[i] = rnd.Next(0, 100);
            }
        }

        static void Sorting(int[] arr)
        {
            int from = 0;
            int before = arr.Length;

            while (true)
            {
                SortRight(arr, from, before);
                before--;
                SortLeft(arr, from, before);
                from++;

                if (from == before)
                    break;
            }
        }
        
        static void SortRight(int[] arr, int from, int before)
        {
            for (int i = from; i < before-1; i++)
            {

                if ( arr[i] > arr[i + 1])
                {
                    Swap(arr, i, i + 1);
                }
            }
        }
        static void SortLeft(int[] arr, int from, int before)
        {
            for (int i = before; i > from; i--)
            {
                if (arr[i] < arr[i - 1])
                {
                    Swap(arr, i, i - 1);
                }
            }
        }

        static void Swap(int[] arr, int i,int j)
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
