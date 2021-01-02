using System;
using System.Collections.Generic;
using System.Linq;

namespace BreakCycles
{
    class Program
    {
        static Dictionary<string, List<string>> graph;
        static List<KeyValuePair<string, string>> edges;
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            graph = new Dictionary<string, List<string>>();
            edges = new List<KeyValuePair<string, string>>();
            ReadGraph(n);

            var removedEdges = new List<KeyValuePair<string, string>>();
            foreach (var kvp in edges.OrderBy(e => e.Key).ThenBy(e => e.Value))
            {
                var first = kvp.Key;
                var second = kvp.Value;

                graph[first].Remove(second);
                graph[second].Remove(first);

                if(HasPath(first, second))
                {
                    if(!removedEdges.Contains(new KeyValuePair<string, string>(second, first)))
                    {
                        removedEdges.Add(new KeyValuePair<string, string>(first, second));
                    }
                }
                else
                {
                    graph[first].Add(second);
                    graph[second].Add(first);
                }
            }

            Console.WriteLine("Edges to remove: " + removedEdges.Count);
            Console.WriteLine(string.Join("\r\n", removedEdges.Select(e => e.Key + " - " + e.Value)));
        }

        private static bool HasPath(string from, string to)
        {
            var visited = new HashSet<string>();
            var queue = new Queue<string>();
            queue.Enqueue(from);
            visited.Add(from);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if(current == to)
                {
                    return true;
                }

                foreach (var child in graph[current])
                {
                    if (!visited.Contains(child))
                    {
                        visited.Add(child);
                        queue.Enqueue(child);
                    }
                }
            }

            return false;
        }

        private static void ReadGraph(int n)
        {
            for (int i = 0; i < n; i++)
            {
                var tokens = Console.ReadLine()
                    .Split(" -> ");
                var node = tokens[0];
                var children = tokens[1]
                    .Split();
                if (!graph.ContainsKey(node))
                {
                    graph[node] = new List<string>();
                }

                foreach (var child in children)
                {
                    graph[node].Add(child);
                    edges.Add(new KeyValuePair<string, string>(node, child));
                }
            }
        }
    }
}
