using System;
using System.Collections.Generic;
using System.Linq;

namespace TheStoryTelling
{
    class Program
    {
        static Dictionary<string, List<string>> graph;
        static HashSet<string> visited;
        static HashSet<string> cycles;
        static void Main(string[] args)
        {
            visited = new HashSet<string>();
            cycles = new HashSet<string>();
            graph = ReadGraph();

            try
            {
                var sorted = TopSort();
                Console.WriteLine(string.Join(" ", sorted));
            }
            catch (InvalidOperationException ioe)
            {
                Console.WriteLine(ioe.Message);
            }

        }

        private static List<string> TopSort()
        {
            var result = new List<string>();

            foreach (var node in graph.Keys)
            {
                TopSortDFS(node, result);
            }

            return result;
        }

        private static void TopSortDFS(string node, List<string> result)
        {
            if (cycles.Contains(node))
            {
                throw new InvalidOperationException("Invalid topological sorting");
            }

            if (visited.Contains(node))
            {
                return;
            }

            cycles.Add(node);
            visited.Add(node);

            foreach (var child in graph[node])
            {
                TopSortDFS(child, result);
            }

            cycles.Remove(node);
            result.Insert(0, node);
        }

        private static Dictionary<string, List<string>> ReadGraph()
        {
            var result = new Dictionary<string, List<string>>();
            var input = Console.ReadLine();
            while (input != "End")
            {
                var tokens = input
                    .Split(" -> ", StringSplitOptions.RemoveEmptyEntries);
                var key = tokens[0];
                List<string> children = null;
                if (tokens.Length > 1)
                {
                    children = tokens[1].Split().ToList();
                }
                else
                {
                    key = key.Split(" ->")[0];
                }

                if (!result.ContainsKey(key))
                {
                    result[key] = new List<string>();
                }

                if (children != null)
                {
                    result[key].AddRange(children);
                }
                input = Console.ReadLine();
            }

            return result;
        }
    }
}
