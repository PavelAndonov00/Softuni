using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Permutations_Without_Repetitions
{
    class Program
    {
        static string[] all;
        static string[] current;
        static bool[] used;
        static int[] numbers;
        static List<int> result;
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            result = new List<int>();
            numbers = new int[n];
            for (int i = 0; i < n; i++)
            {
                numbers[i] = i + 1;
            }
            GenerateVector(-1);


            //all = new string[3] { "A", "B", "b" };
            //PermOptimized(-1);
        }

        private static void GenerateVector(int index)
        {
            if (index != -1)
            {
                result.Add(numbers[index]);
            }

            if (result.Count == numbers.Length)
            {
                Print();
                return;
            }

            for (int i = 0; i < numbers.Length; i++)
            {
                GenerateVector(i);
                result.RemoveAt(result.Count - 1);
            }
        }

        private static void Print()
        {
            Console.WriteLine(string.Join(" ", result));
        }

        private static void PermOptimized(int index)
        {
            if (index == all.Length)
            {
                Print(all);
            }
            else
            {
                PermOptimized(index + 1);
                for (int i = index + 1; i < all.Length; i++)
                {
                    Swap(index, i);
                    PermOptimized(index + 1);
                    Swap(index, i);
                }
            }
        }

        private static void Swap(int index, int i)
        {
            var element = all[index];
            all[index] = all[i];
            all[i] = element;
        }

        private static void Perm(int index)
        {
            if (index == all.Length)
            {
                Print(current);
            }
            else
            {
                for (int i = 0; i < all.Length; i++)
                {
                    if (!used[i])
                    {
                        current[index] = all[i];
                        used[i] = true;
                        Perm(index + 1);
                        used[i] = false;
                    }
                }
            }
        }

        private static void Print(string[] array)
        {
            Console.WriteLine(string.Join(" ", array));
        }
    }
}
