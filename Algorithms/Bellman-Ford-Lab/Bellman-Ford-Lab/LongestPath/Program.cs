using System;
using System.Collections.Generic;
using System.Linq;

namespace LongestPath
{
    class Program
    {
        class Edge
        {
            public int First { get; set; }

            public int Second { get; set; }

            public int Weight { get; set; }
        }
        static Dictionary<int, List<Edge>> nodesToEdges = new Dictionary<int, List<Edge>>();
        static int[] distances;
        static void Main(string[] args)
        {
            ReadInpit();
            distances = new int[nodesToEdges.Keys.Max() + 1];
            foreach (var key in nodesToEdges.Keys)
            {
                distances[key] = int.MinValue;
            }

            var start = int.Parse(Console.ReadLine());
            var end = int.Parse(Console.ReadLine());
            distances[start] = 0;
            var sortedVerticles = TopSort();
            while (sortedVerticles.Count > 0)
            {
                var currentNode = sortedVerticles.Pop();
                foreach (var edge in nodesToEdges[currentNode])
                {
                    var dist = distances[edge.First] + edge.Weight;
                    if(distances[edge.Second] < dist)
                    {
                        distances[edge.Second] = dist;
                    }
                }
            }
            Console.WriteLine(distances[end]);
        }

        private static Stack<int> TopSort()
        {
            var result = new Stack<int>();
            var visited = new bool[nodesToEdges.Keys.Max() + 1];
            foreach (var node in nodesToEdges.Keys)
            {
                DFS(node, visited, result);
            }
            return result;
        }

        private static void DFS(int node, bool[] visited, Stack<int> result)
        {
            if (visited[node])
            {
                return;
            }

            visited[node] = true;

            foreach (var edge in nodesToEdges[node])
            {
                DFS(edge.Second != node ? edge.Second : edge.First, visited, result);
            }

            result.Push(node);
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

                if(!nodesToEdges.ContainsKey(first))
                {
                    nodesToEdges[first] = new List<Edge>();
                }

                if (!nodesToEdges.ContainsKey(second))
                {
                    nodesToEdges[second] = new List<Edge>();
                }
                nodesToEdges[first].Add(edge);
            }
        }
    }
}
