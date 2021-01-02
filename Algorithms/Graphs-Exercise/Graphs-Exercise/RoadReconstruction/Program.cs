using System;
using System.Collections.Generic;
using System.Linq;

namespace RoadReconstruction
{
    class Program
    {
        static List<int>[] graph;
        static List<KeyValuePair<int, int>> edges;
        static void Main(string[] args)
        {
            edges = new List<KeyValuePair<int, int>>();

            var buildings = int.Parse(Console.ReadLine());
            graph = new List<int>[buildings];
            var streets = int.Parse(Console.ReadLine());
            ReadGraph(streets);

            var importantStreets = new List<KeyValuePair<int, int>>();
            foreach (var kvp in edges)
            {
                var first = kvp.Key;
                var second = kvp.Value;

                graph[first].Remove(second);
                graph[second].Remove(first);
                var visited = new HashSet<int>();
                if (!HasPath(first, second, visited))
                {
                    importantStreets.Add(kvp);
                }

                graph[first].Add(second);
                graph[second].Add(first);
            }

            for (int i = 0; i < importantStreets.Count; i++)
            {
                var currentStreet = importantStreets[i];
                if(currentStreet.Key > currentStreet.Value)
                {
                    importantStreets[i] = new KeyValuePair<int, int>(currentStreet.Value, currentStreet.Key);
                }
            }

            Console.WriteLine("Important streets:");
            foreach (var importantStreet in importantStreets)
            {
                if(importantStreet.Key > importantStreet.Value)
                {
                    Console.WriteLine($"{importantStreet.Value} {importantStreet.Key}");
                }
                else
                {
                    Console.WriteLine($"{importantStreet.Key} {importantStreet.Value}");
                }
            }
        }

        private static bool HasPath(int start, int end, HashSet<int> visited)
        {
            if (visited.Contains(start))
            {
                return false;
            }

            if (start == end)
            {
                return true;
            }

            visited.Add(start);
            foreach (var child in graph[start])
            {
                var result = HasPath(child, end, visited);
                if (result)
                {
                    return result;
                }
            }

            return false;
        }

        private static void ReadGraph(int streets)
        {
            for (int i = 0; i < streets; i++)
            {
                var tokens = Console.ReadLine()
                    .Split(" - ")
                    .Select(int.Parse)
                    .ToArray();
                var node = tokens[0];
                var child = tokens[1];

                if (graph[node] == null)
                {
                    graph[node] = new List<int>();
                }

                if (graph[child] == null)
                {
                    graph[child] = new List<int>();
                }

                graph[node].Add(child);
                graph[child].Add(node);
                edges.Add(new KeyValuePair<int, int>(node, child));
            }
        }
    }
}
