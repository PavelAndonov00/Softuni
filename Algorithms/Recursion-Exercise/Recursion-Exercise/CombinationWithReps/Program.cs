using System;
using System.Collections.Generic;

namespace CombinationWithReps
{
    class Program
    {
        static int n;
        static int k;
        static List<int> list;
        static void Main(string[] args)
        {
            list = new List<int>();
            n = int.Parse(Console.ReadLine());
            k = int.Parse(Console.ReadLine());
            CombinationWithRep(0);
        }

        private static void CombinationWithRep(int number)
        {
            if (list.Count == k)
            {
                Console.WriteLine(String.Join(" ", list));
                return;
            }

            for (int r = number + 1; r <= n; r++)
            {
                list.Add(r);
                CombinationWithRep(r);
                list.RemoveAt(list.Count - 1);
            }
        }
    }
}