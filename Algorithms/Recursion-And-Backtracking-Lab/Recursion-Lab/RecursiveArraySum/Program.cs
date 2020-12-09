using System;
using System.Linq;

namespace RecursiveArraySum
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            var sum = GetSum(input, 0);
            Console.WriteLine(sum);
        }

        private static int GetSum(int[] arr, int index)
        {
            if(index > arr.Length - 1)
            {
                return 0;
            }
            var cur = arr[index] + GetSum(arr, index + 1);

            return cur;
        }
    }
}
