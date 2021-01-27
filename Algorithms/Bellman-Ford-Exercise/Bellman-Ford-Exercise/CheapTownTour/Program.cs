using System;
using System.Collections.Generic;
using System.Linq;

namespace CheapTownTour
{
    class Program
    {
        class Edge
        {
            public int First { get; set; }

            public int Second { get; set; }

            public int Weight { get; set; }
        }

        static List<Edge> edges;
        static int[] parents;
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            edges = new List<Edge>();
            ReadGraph();

            edges = edges.OrderBy(e => e.Weight).ToList();
            parents = new int[n];
            for (int i = 0; i < n; i++)
            {
                parents[i] = i;
            }
            var sum = 0;
            foreach (var edge in edges)
            {
                var first = edge.First;
                var second = edge.Second;
                var firstRoot = FindRoot(first);
                var secondRoot = FindRoot(second);

                if(firstRoot != secondRoot)
                {
                    sum += edge.Weight;
                    parents[firstRoot] = secondRoot;
                }
            }

            Console.WriteLine($"Total cost: {sum}");
        }

        private static int FindRoot(int node)
        {
            while(node != parents[node])
            {
                node = parents[node];
            }

            return node;
        }

        private static void ReadGraph()
        {
            var e = int.Parse(Console.ReadLine());
            for (int i = 0; i < e; i++)
            {
                var tokens = Console.ReadLine()
                    .Split(new char[] { ' ', '-' }, StringSplitOptions.RemoveEmptyEntries);
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
