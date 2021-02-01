using System;
using System.Collections.Generic;
using System.Linq;

namespace ArticulationPoints
{
    class Program
    {
        static HashSet<int>[] graph;
        static int[] depths;
        static int[] lowpoints;
        static bool[] visited;
        static int[] prev;
        static List<int> articulationPoints = new List<int>();
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            graph = new HashSet<int>[n];
            depths = new int[n];
            lowpoints = new int[n];
            visited = new bool[n];
            prev = new int[n];
            for (int i = 0; i < n; i++)
            {
                prev[i] = -1;
                depths[i] = -1;
                lowpoints[i] = -1;
                graph[i] = new HashSet<int>();
            }

            ReadGraph();

            FindArticulationPoints(0, 1);
            Console.WriteLine("Articulation points: " + string.Join(", ", articulationPoints));
        }

        private static void FindArticulationPoints(int node, int depth)
        {
            visited[node] = true;
            depths[node] = depth;
            lowpoints[node] = depth;
            var childCount = 0;
            var isArticulationPoint = false;
            foreach (var child in graph[node])
            {
                if (!visited[child])
                {
                    prev[child] = node;
                    FindArticulationPoints(child, depth + 1);
                    childCount++;
                    
                    if(lowpoints[child] >= depth)
                    {
                        isArticulationPoint = true;
                    }

                    lowpoints[node] = Math.Min(lowpoints[node], lowpoints[child]);
                }
                else if(child != prev[node])
                {
                    lowpoints[node] = Math.Min(lowpoints[node], depths[child]);
                }
            }

            if((prev[node] == -1 && childCount > 1) ||
                (prev[node] != -1 && isArticulationPoint))
            {
                articulationPoints.Add(node);
            }
        }

        private static void ReadGraph()
        {
            var l = int.Parse(Console.ReadLine());
            for (int i = 0; i < l; i++)
            {
                var tokens = Console.ReadLine()
                    .Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var node = tokens[0];
                for (int k = 0; k < tokens.Length; k++)
                {
                    var child = tokens[k];
                    graph[node].Add(child);
                    graph[child].Add(node);
                }
            }
        }
    }
}
