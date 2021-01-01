using System;
using System.Collections.Generic;
using System.Linq;

namespace ShortestPath
{
    class Program
    {
        static List<int>[] graph;
        static HashSet<int> visited;
        static int[] parents;
        static void Main(string[] args)
        {
            var nodesCount = int.Parse(Console.ReadLine());
            var edgesCount = int.Parse(Console.ReadLine());
            graph = ReadGraph(nodesCount, edgesCount);

            var startNode = int.Parse(Console.ReadLine());
            var endNode = int.Parse(Console.ReadLine());
            visited = new HashSet<int>();
            parents = new int[nodesCount + 1];
            Array.Fill(parents, -1);
            BFS(startNode, endNode);

            var path = ReconstructPath(endNode);
            Console.WriteLine("Shortest path length is: " + (path.Count - 1));
            Console.WriteLine(string.Join(" ", path));
        }

        private static void BFS(int startNode, int endNode)
        {
            if (visited.Contains(startNode))
            {
                return;
            }

            var queue = new Queue<int>();
            queue.Enqueue(startNode);
            visited.Add(startNode);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (current == endNode)
                {
                    break;
                }

                foreach (var child in graph[current])
                {
                    if (!visited.Contains(child))
                    {
                        queue.Enqueue(child);
                        visited.Add(child);
                        parents[child] = current;
                    }
                }
            }
            ;
        }

        private static Stack<int> ReconstructPath(int endNode)
        {
            var path = new Stack<int>();
            var index = endNode;
            while (index != -1)
            {
                path.Push(index);
                index = parents[index];
            }

            return path;
        }

        private static List<int>[] ReadGraph(int nodesCount, int edgesCount)
        {
            var result = new List<int>[nodesCount + 1];
            for (int i = 0; i < edgesCount; i++)
            {
                var tokens = Console.ReadLine().Split();
                var dest = int.Parse(tokens[1]);
                var source = int.Parse(tokens[0]);

                if (result[source] == null)
                {
                    result[source] = new List<int>();
                }

                result[source].Add(dest);
            }

            for(var i = 1; i < result.Length; i++)
            {
                if(result[i] == null)
                {
                    result[i] = new List<int>();
                }
            }

            return result;
        }
    }
}
