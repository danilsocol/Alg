using System;

namespace BinaryFind_6
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = new int[10];
            FillArr(arr);
            Console.WriteLine(FindNum(arr, 4)); 
        }

        static void FillArr(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = i*i;
            }
        }

        static int FindNum(int[] arr,int num)
        {
            int from = 0;
            int before = arr.Length;

            while (true)
            {
                int aver = (before + from) / 2;
                if (num == arr[aver])
                {
                    return aver;
                }
                else if (num > arr[aver])
                {
                    from = aver;
                }
                else
                {
                    before = aver;
                }
            }
        }

    }
}
