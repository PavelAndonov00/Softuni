using System;
using System.Linq;

namespace BubbleSort
{
    class Program
    {
        static void Main(string[] args)
        {
            var array = Console.ReadLine().Split().Select(int.Parse).ToArray();
            BubbleSort(array);
            Console.WriteLine(string.Join(" ", array));
        }

        private static void BubbleSort(int[] array)
        {
            bool sorted;
            for (int i = 0; i < array.Length; i++)
            {
                sorted = true;
                for (int k = 1; k < array.Length - i; k++)
                {
                    if (array[k - 1] > array[k])
                    {
                        Swap(array, k - 1, k);
                        sorted = false;
                    }
                }

                if (sorted)
                {
                    break;
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
