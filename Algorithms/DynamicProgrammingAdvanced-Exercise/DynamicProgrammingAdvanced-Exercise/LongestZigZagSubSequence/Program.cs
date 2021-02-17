using System;
using System.Collections.Generic;
using System.Linq;

namespace LongestZigZagSubSequence
{
    class Program
    {
        static int[] sequence;
        static List<int> result = new List<int>();
        static List<int> lastResult = new List<int>();
        static int lastIndex;
        static int counter;
        static void Main(string[] args)
        {
            sequence = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            result.Add(sequence[0]);
            if(sequence[0] > sequence[1])
            {
                result.Add(sequence[1]);
                GetLongestZigZagSubSequence(2, true);
            }
            else
            {
                result.Add(sequence[1]);
                GetLongestZigZagSubSequence(2, false);
            }

            Console.WriteLine(string.Join(" ", lastResult));
            Console.WriteLine(counter);
        }

        private static void GetLongestZigZagSubSequence(int index, bool greaterThan)
        {
            counter++;
            if(index > sequence.Length - 1)
            {
                //left most
                if (result.Count > lastResult.Count)
                {
                    lastResult = new List<int>();
                    for (int i = 0; i < result.Count; i++)
                    {
                        lastResult.Add(result[i]);
                    }
                }

                if (lastIndex == sequence.Length - 1 )
                {
                    return;
                }
                
                result.RemoveAt(result.Count - 1);
                GetLongestZigZagSubSequence(lastIndex + 1, !greaterThan);
            }
            else
            {
                if (greaterThan && result.Last() < sequence[index])
                {
                    lastIndex = index;
                    result.Add(sequence[index]);
                    GetLongestZigZagSubSequence(index + 1, !greaterThan);
                }
                else if (!greaterThan && result.Last() > sequence[index])
                {
                    lastIndex = index;
                    result.Add(sequence[index]);
                    GetLongestZigZagSubSequence(index + 1, !greaterThan);
                }
                else
                {
                    GetLongestZigZagSubSequence(index + 1, greaterThan);
                }
            }
        }
    }
}
