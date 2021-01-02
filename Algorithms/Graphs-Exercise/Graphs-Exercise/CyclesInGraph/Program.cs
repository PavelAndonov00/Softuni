using System;
using System.Collections.Generic;

namespace CyclesInGraph
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

            graph = new Dictionary<string, List<string>>();
            PopulateGraph();

            foreach (var node in graph.Keys)
            {
                if (!visited.Contains(node))
                {
                    try
                    {
                        DFS(node);
                    }
                    catch (InvalidOperationException ioe)
                    {
                        Console.WriteLine(ioe.Message);
                        return;
                    }
                }
            }

            Console.WriteLine("Acyclic: Yes");
        }

        private static void DFS(string node)
        {
            if (cycles.Contains(node))
            {
                throw new InvalidOperationException("Acyclic: No");
            }

            if (visited.Contains(node))
            {
                return;
            }

            cycles.Add(node);
            visited.Add(node);

            var children = graph[node];
            foreach (var child in children)
            {
                DFS(child);
            }

            cycles.Remove(node);
        }

        private static void PopulateGraph()
        {
            var input = Console.ReadLine();
            while (input != "End")
            {
                var tokens = input.Split("-");
                var node = tokens[0];
                var child = tokens[1];
                if (!graph.ContainsKey(node))
                {
                    graph[node] = new List<string>();
                }

                if (!graph.ContainsKey(child))
                {
                    graph[child] = new List<string>();
                }

                graph[node].Add(child);
                input = Console.ReadLine();
            }
        }
    }
}
