using System;
using System.Linq;

namespace SelectionSort
{
    class Program
    {
        static void Main(string[] args)
        {
            var array = Console.ReadLine().Split().Select(int.Parse).ToArray();
            SelectionSort(array);
            Console.WriteLine(string.Join(" ", array));
        }

        private static void SelectionSort(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                var first = array[i];
                var min = first;
                var minIndex = -1;
                for (int k = i + 1; k < array.Length; k++)
                {
                    var second = array[k];
                    if (second <= min)
                    {
                        min = second;
                        minIndex = k;
                    }
                }

                if (minIndex != -1)
                {
                    array[i] = array[minIndex];
                    array[minIndex] = first;
                }
            }
        }
    }
}
