using System;
using System.Linq;

namespace BinarySearch
{
    class Program
    {
        static void Main(string[] args)
        {
            var array = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            var number = int.Parse(Console.ReadLine());
            Console.WriteLine(BinarySearch(array, number, 0, array.Length - 1));
        }

        private static int BinarySearch(int[] array, int number, int startIndex, int endIndex)
        {
            if (startIndex > endIndex) return -1;

            var middle = (startIndex + endIndex) / 2;
            var element = array[middle];
            if (element == number)
            {
                return middle;
            }
            else if (element > number)
            {
                return BinarySearch(array, number, startIndex, middle - 1);
            }
            else
            {
                return BinarySearch(array, number, middle + 1, endIndex);
            }
        }
    }
}
