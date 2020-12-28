using System;
using System.Linq;

namespace QuickSort
{
    class Program
    {
        static void Main(string[] args)
        {
            var array = Console.ReadLine().Split().Select(int.Parse).ToArray();
            QuickSort(array, 0, array.Length - 1);
            Console.WriteLine(string.Join(" ", array));
        }

        private static void QuickSort(int[] array, int left, int right)
        {
            if (left < right)
            {
                var pivot = left;
                var storedIndex = pivot + 1;
                for (int i = pivot + 1; i <= right; i++)
                {
                    if (array[i] < array[pivot])
                    {
                        Swap(array, i, storedIndex);
                        storedIndex++;
                    }
                }

                Swap(array, pivot, storedIndex - 1);
                QuickSort(array, storedIndex, right);
                QuickSort(array, left, storedIndex - 2);

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
