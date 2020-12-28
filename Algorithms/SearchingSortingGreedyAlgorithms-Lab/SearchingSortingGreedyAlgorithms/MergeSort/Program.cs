using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Merge_sort
{
    class Program
    {
        static void Main(string[] args)
        {
            var unsorted = Console.ReadLine().Split().Select(int.Parse).ToList();
            List<int> sorted = MergeSort(unsorted);
            Console.WriteLine(string.Join(" ", sorted));
        }


        private static List<int> MergeSort(List<int> list)
        {
            if(list.Count <= 1)
            {
                return list;
            }

            var left = list.Take(list.Count / 2).ToList();
            var right = list.Skip(list.Count / 2).ToList();
            var leftDivided = MergeSort(left);
            var rightDivided = MergeSort(right);

            var merged = Merge(leftDivided, rightDivided);
            return merged;
        }

        private static List<int> Merge(List<int> leftDivided, List<int> rightDivided)
        {
            var merged = new List<int>();
            while (leftDivided.Count > 0 || rightDivided.Count > 0)
            {
                if(leftDivided.Count > 0 && rightDivided.Count > 0)
                {
                    var leftFirst = leftDivided.First();
                    var rightFirst = rightDivided.First();
                    if (leftFirst <= rightFirst)
                    {
                        merged.Add(leftFirst);
                        leftDivided.Remove(leftFirst);
                    }
                    else
                    {
                        merged.Add(rightFirst);
                        rightDivided.Remove(rightFirst);
                    }
                }
                else if(leftDivided.Count > 0)
                {
                    var leftFirst = leftDivided.First();
                    merged.Add(leftFirst);
                    leftDivided.Remove(leftFirst);
                }
                else if(rightDivided.Count > 0)
                {
                    var rightFirst = rightDivided.First();
                    merged.Add(rightFirst);
                    rightDivided.Remove(rightFirst);
                }
            }

            return merged;
        }
    }
}