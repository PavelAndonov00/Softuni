using System;
using System.Collections.Generic;
using System.Linq;

namespace SourceRemoval
{
    class Program
    {
        static Dictionary<string, List<string>> graph;
        static Dictionary<string, int> predecessorCount;
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            graph = ReadGraph(n);
            predecessorCount = ReadDependancies();
            var sorted = TopologicalSorting();
            if (sorted == null)
            {
                Console.WriteLine("Invalid topological sorting");
            }
            else
            {
                Console.WriteLine("Topological sorting: " + string.Join(", ", sorted));
            }
        }

        private static List<string> TopologicalSorting()
        {
            var result = new List<string>();

            while (predecessorCount.Count > 0)
            {
                var kvp = predecessorCount
                    .FirstOrDefault(n => n.Value == 0);

                var node = kvp.Key;
                if (node == null)
                {
                    break;
                }

                result.Add(node);
                foreach (var child in graph[node])
                {
                    predecessorCount[child]--;
                }

                predecessorCount.Remove(node);
            }

            if (predecessorCount.Count > 0)
            {
                return null;
            }

            return result;
        }

        private static Dictionary<string, int> ReadDependancies()
        {
            var result = new Dictionary<string, int>();

            foreach (var kvp in graph)
            {
                var key = kvp.Key;
                var children = kvp.Value;

                if (!result.ContainsKey(key))
                {
                    result[key] = 0;
                }

                foreach (var child in children)
                {
                    if (!result.ContainsKey(child))
                    {
                        result[child] = 0;
                    }

                    result[child]++;
                }
            }

            return result;
        }

        private static Dictionary<string, List<string>> ReadGraph(int n)
        {
            var result = new Dictionary<string, List<string>>();
            for (int i = 0; i < n; i++)
            {
                var input = Console.ReadLine()
                    .Split(" -> ", StringSplitOptions.RemoveEmptyEntries);
                var key = input[0];

                List<string> children = new List<string>();
                if (input.Length > 1)
                {
                    children = input[1].Split(", ").ToList();
                }
                else
                {
                    key = key.Split(" ->")[0];
                }

                result[key] = children;
            }

            return result;
        }
    }
}
