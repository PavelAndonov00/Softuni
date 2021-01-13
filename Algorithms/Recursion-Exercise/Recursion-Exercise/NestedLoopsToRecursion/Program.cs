using System;
using System.Collections.Generic;

namespace NestedLoopsToRecursion
{
    class Program
    {
        static List<int> list;
        static void Main(string[] args)
        {
            list = new List<int>();
            var n = int.Parse(Console.ReadLine());
            Loop(n);
        }

        private static void Loop(int n)
        {
            if(list.Count == n)
            {
                Console.WriteLine(string.Join(" ", list));
                return;
            }

            for (int i = 1; i <= n; i++)
            {
                list.Add(i);
                Loop(n);
                list.RemoveAt(list.Count - 1);
            }
        }
    }
}
