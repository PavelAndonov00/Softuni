using System;
using System.Collections.Generic;
using System.Linq;

namespace DistanceBetweenVerticales
{
    class Program
    {
        static Dictionary<int, List<int>> graph;
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var p = int.Parse(Console.ReadLine());
            graph = ReadGraph(n);
            var distances = new List<KeyValuePair<int, int>>();
            for (int i = 0; i < p; i++)
            {
                var tokens = Console.ReadLine().Split("-");
                var start = int.Parse(tokens[0]);
                var end = int.Parse(tokens[1]);

                distances.Add(new KeyValuePair<int, int>(start, end));
            }

            foreach (var kvp in distances)
            {
                var start = kvp.Key;
                var end = kvp.Value;

                int length = -1;
                BFS(start, end, ref length);
                Console.WriteLine($"{{{start}, {end}}} -> {length}");
            }
        }

        private static int ReconstructPath(int end, Dictionary<int, int> parents)
        {
            var result = new List<int>();
            var index = end;
            while (index != 0)
            {
                result.Add(index);
                if (!parents.ContainsKey(index))
                {
                    break;
                }
                index = parents[index];
            }

            return result.Count - 1;
        }

        private static void BFS(int start, int end, ref int length)
        {
            var visited = new HashSet<int>();
            var parents = new Dictionary<int, int>();
            var queue = new Queue<int>();
            queue.Enqueue(start);
            visited.Add(start);

            var found = false;
            while (queue.Count > 0)
            {
                var currentNode = queue.Dequeue();

                if (currentNode == end)
                {
                    found = true;
                    break;
                }

                foreach (var child in graph[currentNode])
                {
                    if (!visited.Contains(child))
                    {
                        visited.Add(child);
                        parents[child] = currentNode;
                        queue.Enqueue(child);
                    }
                }
            }

            if (found)
            {
                length = ReconstructPath(end, parents);
            }
        }

        private static Dictionary<int, List<int>> ReadGraph(int n)
        {
            var result = new Dictionary<int, List<int>>();
            for (int i = 0; i < n; i++)
            {
                var tokens = Console.ReadLine().Split(":", StringSplitOptions.RemoveEmptyEntries);
                var node = int.Parse(tokens[0]);
                if (tokens.Length > 1)
                {
                    result[node] = tokens[1].Split().Select(int.Parse).ToList();
                }
                else
                {
                    result[node] = new List<int>();
                }
            }

            return result;
        }
    }
}
