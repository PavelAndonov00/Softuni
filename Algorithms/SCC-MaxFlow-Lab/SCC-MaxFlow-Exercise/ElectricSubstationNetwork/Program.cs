using System;
using System.Collections.Generic;
using System.Linq;

namespace ElectricSubstationNetwork
{
    class Program
    {
        static List<int>[] graph;
        static List<int>[] reversedGraph;
        static Stack<int> stack = new Stack<int>();
        static bool[] visited;
        static List<Stack<int>> scc = new List<Stack<int>>();
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            graph = new List<int>[n];
            reversedGraph = new List<int>[n];
            for (int i = 0; i < n; i++)
            {
                graph[i] = new List<int>();
                reversedGraph[i] = new List<int>();
            }
            ReadGraph();

            visited = new bool[n];
            for (int i = 0; i < n; i++)
            {
                if (!visited[i])
                {
                    DFS(i);
                }
            }

            visited = new bool[n];
            while (stack.Count > 0)
            {
                var current = stack.Pop();
                if (!visited[current])
                {
                    scc.Add(new Stack<int>());
                    DFS_ON_Reversed(current);
                }
            }

            Console.WriteLine(string.Join("\r\n", scc.Select(s => string.Join(", ", s))));
        }

        private static void DFS_ON_Reversed(int node)
        {
            if (!visited[node])
            {
                visited[node] = true;
                foreach (var child in graph[node])
                {
                    DFS_ON_Reversed(child);
                }

                scc.Last().Push(node);
            }
        }

        private static void DFS(int node)
        {
            if (!visited[node])
            {
                visited[node] = true;
                foreach (var child in graph[node])
                {
                    DFS(child);
                }

                stack.Push(node);
            }
        }

        private static void ReadGraph()
        {
            var l = int.Parse(Console.ReadLine());
            for (int i = 0; i < l; i++)
            {
                var tokens = Console.ReadLine()
                    .Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var node = tokens[0];
                for (int k = 1; k < tokens.Length; k++)
                {
                    graph[node].Add(tokens[k]);
                    reversedGraph[tokens[k]].Add(node);
                }
            }
        }
    }
}
