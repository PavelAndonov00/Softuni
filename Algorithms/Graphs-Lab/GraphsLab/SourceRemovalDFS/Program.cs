using System;
using System.Collections.Generic;
using System.Linq;

namespace SourceRemovalDFS
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
            var n = int.Parse(Console.ReadLine());
            graph = ReadGraph(n);

            try
            {
                var sorted = TopSort();
                Console.WriteLine("Topological sorting: " + string.Join(", ", sorted));
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
