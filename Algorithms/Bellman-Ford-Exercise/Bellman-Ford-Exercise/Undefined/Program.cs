using System;
using System.Collections.Generic;

namespace Undefined
{
    class Program
    {
        class Edge
        {
            public int First { get; set; }

            public int Second { get; set; }

            public int Weight { get; set; }
        }

        static List<Edge> edges = new List<Edge>();
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            ReadGraph();

            var distances = new double[n + 1];
            var prev = new int[n + 1];
            for (int i = 1; i <= n; i++)
            {
                distances[i] = double.PositiveInfinity;
                prev[i] = -1;
            }

            var start = int.Parse(Console.ReadLine());
            var end = int.Parse(Console.ReadLine());

            distances[start] = 0;
            var updated = false;
            for (int i = 1; i < n-1; i++)
            {
                updated = false;
                foreach (var edge in edges)
                {
                    if (double.IsPositiveInfinity(distances[edge.First]))
                    {
                        continue;
                    }

                    var dist = distances[edge.First] + edge.Weight;
                    if(dist < distances[edge.Second])
                    {
                        distances[edge.Second] = dist;
                        prev[edge.Second] = edge.First;
                        updated = true;
                    }
                }

                if (!updated)
                {
                    break;
                }
            }

            updated = false;
            foreach (var edge in edges)
            {
                if (double.IsPositiveInfinity(distances[edge.First]))
                {
                    continue;
                }

                var dist = distances[edge.First] + edge.Weight;
                if (dist < distances[edge.Second])
                {
                    distances[edge.Second] = dist;
                    updated = true;
                }
            }

            if (updated)
            {
                Console.WriteLine("Undefined");
            }
            else
            {
                Stack<int> result = new Stack<int>();
                var currentNode = end;
                while (currentNode != -1)
                {
                    result.Push(currentNode);
                    currentNode = prev[currentNode];
                }

                Console.WriteLine(string.Join(" ", result));
                Console.WriteLine(distances[end]);
            }
        }

        private static void ReadGraph()
        {
            var e = int.Parse(Console.ReadLine());
            for (int i = 0; i < e; i++)
            {
                var tokens = Console.ReadLine()
                    .Split(' ');
                var first = int.Parse(tokens[0]);
                var second = int.Parse(tokens[1]);
                var weight = int.Parse(tokens[2]);

                var edge = new Edge()
                {
                    First = first,
                    Second = second,
                    Weight = weight
                };
                edges.Add(edge);
            }
        }
    }
}
