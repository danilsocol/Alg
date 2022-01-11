using System;

namespace RadixSort_10
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
        public static void Sorting(int[] arr)
        {
            int i, j;
            int[] tmp = new int[arr.Length];
            for (int shift = sizeof(int) * 8 - 1; shift >= 0; shift--)
            {
                j = 0;
                for (i = 0; i < arr.Length; i++)
                {
                    bool move = (arr[i] << shift) >= 0;
                    if (shift == 0 ? !move : move)
                        arr[i - j] = arr[i];
                    else
                        tmp[j++] = arr[i];
                }
                Array.Copy(tmp, 0, arr, arr.Length - j, j);
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
