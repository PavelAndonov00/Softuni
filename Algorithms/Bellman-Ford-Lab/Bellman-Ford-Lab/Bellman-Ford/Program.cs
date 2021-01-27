using System;
using System.Collections.Generic;
using System.Linq;

namespace Bellman_Ford
{
    
    class Program
    {
        class Edge
        {
            public int First { get; set; }

            public int Second { get; set; }

            public int Weight { get; set; }
        }

        static HashSet<int> nodes;
        static List<Edge> edges;
        static double[] distances;
        static int[] prev;

        static void Main(string[] args)
        {
            nodes = new HashSet<int>();
            edges = new List<Edge>();
            ReadInpit();

            distances = new double[nodes.Max() + 1];
            prev = new int[nodes.Max() + 1];
            foreach (var node in nodes)
            {
                distances[node] = double.PositiveInfinity;
                prev[node] = -1;
            }

            var startNode = int.Parse(Console.ReadLine());
            var endNode = int.Parse(Console.ReadLine());

            distances[startNode] = 0;
            for (int i = 0; i < nodes.Count - 1; i++)
            {
                var updated = false;
                foreach (var edge in edges)
                {
                    if (double.IsPositiveInfinity(edge.First))
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

            foreach (var edge in edges)
            {
                if (double.IsPositiveInfinity(edge.First))
                {
                    continue;
                }

                var dist = distances[edge.First] + edge.Weight;
                if (dist < distances[edge.Second])
                {
                    Console.WriteLine("Negative Cycle Detected");
                    return;
                }
            }

            var previous = prev[endNode];
            var result = new Stack<int>();
            result.Push(endNode);
            while (previous != -1)
            {
                result.Push(previous);
                previous = prev[previous];
            }
            Console.WriteLine(string.Join(" ", result));
            Console.WriteLine(distances[endNode]);
        }

        private static void ReadInpit()
        {
            var numberOfNodes = int.Parse(Console.ReadLine());
            var numberOfEdges = int.Parse(Console.ReadLine());
            for (int i = 0; i < numberOfEdges; i++)
            {
                var tokens = Console.ReadLine()
                    .Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var first = int.Parse(tokens[0]);
                var second = int.Parse(tokens[1]);
                var weight = int.Parse(tokens[2]);

                var edge = new Edge
                {
                    First = first,
                    Second = second,
                    Weight = weight
                };
                edges.Add(edge);
                nodes.Add(first);
                nodes.Add(second);
            }
        }
    }
}
