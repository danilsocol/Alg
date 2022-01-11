using System;

namespace SelectSort_3
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
            for (int i = 0; i < arr.Length; i++)
            {
                int minIndex = FindMin(arr,i);
                Swap(arr, i, minIndex);
            }
        }

        static void Swap(int[] arr, int i, int j)
        {
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

        static int FindMin(int[] arr,int i)
        {
            int min = arr[i];
            int minIndex = i;

            for (int j = i+1; j < arr.Length; j++)
            {
                if(min > arr[j])
                {
                    min = arr[j];
                    minIndex = j;
                }
            }
            return minIndex;
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
