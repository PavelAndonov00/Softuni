using System;
using System.Collections.Generic;
using System.Linq;

namespace MaxFlow
{
    class Program
    {
        static int[,] graph;
        static int[] parents;
        static int source;
        static int destination;
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            graph = new int[n, n];
            ReadGraph();

            source = int.Parse(Console.ReadLine());
            destination = int.Parse(Console.ReadLine());

            parents = new int[n];
            for (int i = 0; i < parents.Length; i++)
            {
                parents[i] = -1;
            }

            var maxFlow = 0;
            while (BFS())
            {
                var minFlow = int.MaxValue;
                var to = destination;
                var from = parents[to];

                while (to != -1 && from != -1)
                {
                    minFlow = Math.Min(minFlow, graph[from, to]);

                    to = parents[to];
                    from = parents[to];
                }

                maxFlow += minFlow;

                to = destination;
                from = parents[to];

                while (to != -1 && from != -1)
                {
                    graph[from, to] -= minFlow;

                    to = parents[to];
                    from = parents[to];
                }
            }

            Console.WriteLine("Max flow = " + maxFlow);
        }

        private static bool BFS()
        {
            var visited = new bool[graph.Length];
            var queue = new Queue<int>();
            visited[source] = true;
            queue.Enqueue(source);
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                for (int i = 0; i < graph.GetLength(1); i++)
                {
                    if (!visited[i] && graph[current, i] > 0)
                    {
                        queue.Enqueue(i);
                        visited[i] = true;
                        parents[i] = current;
                    }
                }
            }

            return visited[destination];
        }

        private static void ReadGraph()
        {
            for (int i = 0; i < graph.GetLength(0); i++)
            {
                var row = Console.ReadLine()
                    .Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                for (int k = 0; k < row.Length; k++)
                {
                    graph[i, k] = row[k];
                }
            }
        }
    }
}
