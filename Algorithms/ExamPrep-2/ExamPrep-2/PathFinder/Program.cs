using System;
using System.Collections.Generic;
using System.Linq;

namespace PathFinder
{
    class Program
    {
        static List<int>[] graph;
        static void Main(string[] args)
        {
            var numberOfNodes = int.Parse(Console.ReadLine());
            graph = new List<int>[numberOfNodes];
            for (int graphIndex = 0; graphIndex < numberOfNodes; graphIndex++)
            {
                var children = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToList();
                graph[graphIndex] = children;
            }

            var numberOfPaths = int.Parse(Console.ReadLine());
            var result = new string[numberOfPaths];
            for (int i = 0; i < numberOfPaths; i++)
            {
                var path = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToList();
                var currentNode = path[0];
                var hasPath = true;
                var remainingNodes = path.Skip(1).ToArray();
                foreach (var node in remainingNodes)
                {
                    if (!graph[currentNode].Contains(node))
                    {
                        hasPath = false;
                    }

                    currentNode = node;
                }

                result[i] = hasPath ? "yes" : "no";
            }

            Console.WriteLine(string.Join("\r\n", result));
        }
    }
}
