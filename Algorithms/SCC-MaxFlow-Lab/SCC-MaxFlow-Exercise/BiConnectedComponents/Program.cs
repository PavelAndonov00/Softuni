using System;
using System.Collections.Generic;
using System.Linq;

namespace BiConnectedComponents
{
    class Program
    {
        static List<int>[] graph;
        static int[] depths;
        static int[] lowpoints;
        static int?[] prev;
        static bool[] visited;
        static bool[] visited2;
        static int articulationPointsCount;
        static List<List<int>> articulationPoints = new List<List<int>>();
        static void Main(string[] args)
        {
            var nodesCount = int.Parse(Console.ReadLine());
            graph = new List<int>[nodesCount];
            for (int i = 0; i < nodesCount; i++)
            {
                graph[i] = new List<int>();
            }
            ReadGraph();

            depths = new int[nodesCount];
            lowpoints = new int[nodesCount];
            prev = new int?[nodesCount];
            visited = new bool[nodesCount];
            visited2 = new bool[nodesCount];
            DFS(0, 1);
            Console.WriteLine($"Number of bi-connected components: {articulationPoints.Count}");
        }

        private static void DFS(int node, int depth)
        {
            visited[node] = true;
            visited2[node] = true;
            depths[node] = depth;
            lowpoints[node] = depth;
            var childCount = 0;
            var isArticulationPoint = false;
            foreach (var child in graph[node])
            {
                if (!visited[child])
                {
                    prev[child] = node;
                    DFS(child, depth + 1);
                    childCount++;

                    if (lowpoints[child] >= depth)
                    {
                        articulationPoints.Add(new List<int>());
                        for (int i = 0; i < visited2.Length; i++)
                        {
                            if(i != child && i != node && visited2[i])
                            {
                                articulationPoints.Last().Add(i);
                            }
                        }
                        visited2 = new bool[visited.Length];
                        articulationPointsCount++;
                        isArticulationPoint = true;
                    }

                    lowpoints[node] = Math.Min(lowpoints[node], lowpoints[child]);
                }
                else if (child != prev[node])
                {
                    lowpoints[node] = Math.Min(lowpoints[node], depths[child]);
                }
            }

            if ((prev[node] == null && childCount > 1) ||
                (prev[node] != null && isArticulationPoint)) 
            {
                //If you want articulation points
            }
        }

        private static void ReadGraph()
        {
            var lines = int.Parse(Console.ReadLine());
            for (int i = 0; i < lines; i++)
            {
                var tokens = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();
                var from = tokens[0];
                var to = tokens[1];

                graph[from].Add(to);
                graph[to].Add(from);
            }
        }
    }
}
