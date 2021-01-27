using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace CableNetwork
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
        static List<int> spanningTree = new List<int>();
        static int totalSum = 0;
        static int budgetLimit = 0;
        static void Main(string[] args)
        {
            budgetLimit = int.Parse(Console.ReadLine());
            var n = int.Parse(Console.ReadLine());
            ReadGraph();

            edges = edges.OrderBy(e => e.Weight).ToList();
            while (true)
            {
                var updated = false;
                foreach (var edge in edges)
                {
                    if (edge.Weight + totalSum > budgetLimit) continue;

                    var nonTreeNode = -1;
                    if (spanningTree.Contains(edge.First) && !spanningTree.Contains(edge.Second))
                    {
                        nonTreeNode = edge.Second;
                    }
                    else if (!spanningTree.Contains(edge.First) && spanningTree.Contains(edge.Second))
                    {
                        nonTreeNode = edge.First;
                    }

                    if (nonTreeNode != -1)
                    {
                        totalSum += edge.Weight;
                        spanningTree.Add(nonTreeNode);
                        updated = true;
                        break;
                    }
                }

                if (!updated)
                {
                    break;
                }
            }

            Console.WriteLine("Budget used: " + totalSum);
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
                if (tokens.Length == 4)
                {
                    spanningTree.Add(first);
                    spanningTree.Add(second);
                }
                else
                {
                    var edge = new Edge
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
}
