using System;
using System.Linq;

namespace ReverseArray
{
    class Program
    {
        static void Main(string[] args)
        {
            var array = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();
            ReverseArray(0, array);
            Console.WriteLine(string.Join(" ", array));
        }

        private static void ReverseArray(int left, int[] array)
        {
            if(left == array.Length / 2)
            {
                return;
            }

            var right = array.Length - 1 - left;
            Swap(array, left, right);
            ReverseArray(left + 1, array);
        }

        private static void Swap(int[] array, int first, int second)
        {
            var temp = array[first];
            array[first] = array[second];
            array[second] = temp;
        }
    }
}
