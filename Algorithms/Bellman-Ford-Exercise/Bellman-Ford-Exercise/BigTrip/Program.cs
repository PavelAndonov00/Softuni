using System;
using System.Collections.Generic;

namespace BigTrip
{
    class Program
    {
        class Edge
        {
            public int First { get; set; }

            public int Second { get; set; }

            public int Weight { get; set; }
        }

        static List<Edge>[] edges;
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            edges = new List<Edge>[n + 1];
            ReadGraph();

            var dists = new int[n + 1];
            var prev = new int[n + 1];
            for (int i = 1; i <= n; i++)
            {
                dists[i] = int.MinValue;
                prev[i] = -1;
            }

            var start = int.Parse(Console.ReadLine());
            var end = int.Parse(Console.ReadLine());
            dists[start] = 0;

            var sorted = TopSort();
            while (sorted.Count > 0)
            {
                var current = sorted.Pop();
                foreach (var edge in edges[current])
                {
                    var dist = dists[edge.First] + edge.Weight;
                    if (dists[edge.Second] < dist)
                    {
                        dists[edge.Second] = dist;
                        prev[edge.Second] = edge.First;
                    }
                }
            }

            Console.WriteLine(dists[end]);
            var result = new Stack<int>();
            var currentNode = end;
            while (currentNode != -1)
            {
                result.Push(currentNode);
                currentNode = prev[currentNode];
            }
            Console.WriteLine(string.Join(" ", result));
        }

        private static Stack<int> TopSort()
        {
            var nodes = new Stack<int>();
            var visited = new bool[edges.Length];
            for (int i = 1; i < edges.Length; i++)
            {
                DFS(i, visited, nodes);
            }

            return nodes;
        }

        private static void DFS(int node, bool[] visited, Stack<int> nodes)
        {
            if (visited[node])
            {
                return;
            }

            visited[node] = true;
            foreach (var edge in edges[node])
            {
                DFS(edge.First == node ? edge.Second : edge.First, visited, nodes);
            }

            nodes.Push(node);
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

                if (edges[first] == null)
                {
                    edges[first] = new List<Edge>();
                }

                if (edges[second] == null)
                {
                    edges[second] = new List<Edge>();
                }

                edges[first].Add(edge);
            }
        }
    }
}
