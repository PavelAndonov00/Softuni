using System;
using System.Collections.Generic;
using System.Linq;

namespace StronglyConnectedComponents
{
    class Program
    {
        static List<int>[] graph;
        static bool[] visited;
        static Stack<int> topSortedGraph = new Stack<int>();
        static List<int>[] reversedGraph;
        static List<List<int>> stronglyConnectedComps = new List<List<int>>();
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
            for (int node = 0; node < n; node++)
            {
                if (!visited[node])
                {
                    DFS(node);
                }
            }

            BuildReversedGraph();

            visited = new bool[n];
            while (topSortedGraph.Count > 0)
            {
                var node = topSortedGraph.Pop();
                if (!visited[node])
                {
                    stronglyConnectedComps.Add(new List<int>());
                    ReversedDFS(node);
                }
            }

            Console.WriteLine("Strongly Connected Components:");
            foreach (var scc in stronglyConnectedComps)
            {
                scc.Reverse();
                Console.WriteLine("{" + string.Join(", ", scc) + "}");
            }
        }

        private static void BuildReversedGraph()
        {
            for (int node = 0; node < graph.Length; node++)
            {
                foreach (var child in graph[node])
                {
                    reversedGraph[child].Add(node);
                }
            }
        }

        private static void ReversedDFS(int node)
        {
            if(!visited[node])
            {
                visited[node] = true;

                foreach (var child in reversedGraph[node])
                {
                    ReversedDFS(child);
                }

                stronglyConnectedComps.Last().Add(node);
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

                topSortedGraph.Push(node);
            }
        }

        private static void ReadGraph()
        {
            var l = int.Parse(Console.ReadLine());
            for (int i = 0; i < l; i++)
            {
                var tokens = Console.ReadLine()
                    .Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                var node = int.Parse(tokens[0]);
                if(graph[node] == null)
                {
                    graph[node] = new List<int>();
                }

                graph[node].AddRange(tokens.Skip(1).Select(int.Parse));
            }
        }
    }
}
