using System;
using System.Collections.Generic;
using System.Linq;

namespace Boxes
{
    class Program
    {
        class Box
        {
            public int Width { get; set; }

            public int Depth { get; set; }

            public int Height { get; set; }
        }
        static Box[] boxes;
        static int[] len;
        static int[] prev;
        static void Main(string[] args)
        {
            ReadInputAndInitialize();
            FindLISAndPrint();
        }

        private static void FindLISAndPrint()
        {
            var bestLength = 0;
            var lastIndex = 0;
            for (int curIdx = 0; curIdx < boxes.Length; curIdx++)
            {
                var currentBox = boxes[curIdx];
                var currentLength = 1;
                var prevIndex = -1;
                for (int prevIdx = curIdx - 1; prevIdx >= 0; prevIdx--)
                {
                    var previousBox = boxes[prevIdx];
                    // len[prevIdx] + 1 ---> Length of the previous plus current
                    // len[prevIdx] + 1 >= currentLength ---> finding left-most
                    if (currentBox.Width > previousBox.Width &&
                        currentBox.Depth > previousBox.Depth &&
                        currentBox.Height > previousBox.Height
                        && len[prevIdx] + 1 >= currentLength)
                    {
                        currentLength = len[prevIdx] + 1;
                        prevIndex = prevIdx;
                    }
                }

                len[curIdx] = currentLength;
                prev[curIdx] = prevIndex;

                // Storing best length to obtain the last index of the obtained max length ---> lastIndex = curIdx
                if (bestLength < currentLength)
                {
                    bestLength = currentLength;
                    lastIndex = curIdx;
                }
            }

            var result = new Stack<int>();
            result.Push(lastIndex);
            while (prev[lastIndex] != -1)
            {
                result.Push(prev[lastIndex]);
                lastIndex = prev[lastIndex];
            }
            foreach (var index in result)
            {
                var currentBox = boxes[index];
                Console.WriteLine($"{currentBox.Width} {currentBox.Depth} {currentBox.Height}");
            }
        }

        private static void ReadInputAndInitialize()
        {
            var n = int.Parse(Console.ReadLine());
            boxes = new Box[n];
            len = new int[n];
            prev = new int[n];
            for (int i = 0; i < n; i++)
            {
                prev[i] = -1;
                var tokens = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();
                var width = tokens[0];
                var depth = tokens[1];
                var height = tokens[2];

                boxes[i] = new Box
                {
                    Width = width,
                    Depth = depth,
                    Height = height
                };
                
            }
        }
    }
}
