using System;

namespace ShellSort_5
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
            int j;
            int step = arr.Length / 2;
            while (step > 0)
            {
                for (int i = 0; i < (arr.Length - step); i++)
                {
                    j = i;
                    while ((j >= 0) && (arr[j] > arr[j + step]))
                    {

                        Swap(arr, j, j + step);
                        j -= step;
                    }
                }
                step = step / 2;
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
