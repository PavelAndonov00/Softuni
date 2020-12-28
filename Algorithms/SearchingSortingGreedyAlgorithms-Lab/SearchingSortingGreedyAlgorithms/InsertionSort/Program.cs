using System;
using System.Linq;

namespace InsertionSort
{
    class Program
    {
        static void Main(string[] args)
        {
            var array = Console.ReadLine().Split().Select(int.Parse).ToArray();
            InsertionSort(array);
            Console.WriteLine(string.Join(" ", array));
        }

        private static void InsertionSort(int[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                var temp = i;
                for (int k = i - 1; k >= 0; k--)
                {
                    if (array[temp] < array[k])
                    {
                        Swap(array, temp, k);
                        temp = k;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        private static void Swap(int[] array, int index1, int index2)
        {
            var temp = array[index1];
            array[index1] = array[index2];
            array[index2] = temp;
        }
    }
}
