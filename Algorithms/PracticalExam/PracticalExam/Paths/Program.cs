using System;
using System.Collections.Generic;
using System.Linq;

namespace Paths
{
    class Program
    {
        static List<int>[] graph;
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            graph = new List<int>[n];
            for (int i = 0; i < n; i++)
            {
                var children = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToList();

                graph[i] = children;
            }

            for (int i = 0; i < graph.Length - 1; i++)
            {
                var path = new List<int>();
                FindPaths(i, path);
            }
        }

        private static void FindPaths(int currentNode, List<int> path)
        {
            path.Add(currentNode);

            foreach (var node in graph[currentNode])
            {
                FindPaths(node, path);
                path.Remove(node);
            }

            if (currentNode == graph.Length - 1)
            {
                Console.WriteLine(string.Join(" ", path));
            }
        }
    }
}
