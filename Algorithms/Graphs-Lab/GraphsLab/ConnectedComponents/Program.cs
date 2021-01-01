using System;
using System.Collections.Generic;
using System.Linq;

namespace ConnectedComponents
{
    class Program
    {
        static List<int>[] graph;
        static HashSet<int> visited;
        static void Main(string[] args)
        {
            visited = new HashSet<int>();
            var n = int.Parse(Console.ReadLine());
            graph = new List<int>[n];
            for (int i = 0; i < n; i++)
            {
                var tokens = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                graph[i] = tokens;
            }

            for (int i = 0; i < graph.Length; i++)
            {
                if (!visited.Contains(i))
                {
                    Console.Write("Connected component: ");
                    DFS(i);
                    Console.WriteLine();
                }
            }

        }

        private static void DFS(int child)
        {
            if (visited.Contains(child))
            {
                return;
            }

            visited.Add(child);

            foreach (var ch in graph[child])
            {
                DFS(ch);
            }

            Console.Write(child + " ");
        }
    }
}
