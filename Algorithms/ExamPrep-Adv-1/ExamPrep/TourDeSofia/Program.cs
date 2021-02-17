using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace TourDeSofia
{
    class Program
    {
        class Edge
        {
            public Edge(int from, int to, int weight)
            {
                this.From = from;
                this.To = to;
                this.Weight = weight;
            }

            public int From { get; set; }

            public int To { get; set; }

            public int Weight { get; set; }
        }
        static List<Edge>[] graph;
        static int[] dist;
        static void Main(string[] args)
        {
            Initialize();

            var e = int.Parse(Console.ReadLine());
            var startNode = int.Parse(Console.ReadLine());
            ReadGraph(e);

            dist[startNode] = 0;
            var queue = new SortedSet<int>(Comparer<int>.Create((f, s) => dist[f] - dist[s]));
            queue.Add(startNode);
            while (queue.Count > 0)
            {
                var from = queue.First();
                queue.Remove(from);

                foreach (var edge in graph[from])
                {
                    var to = edge.To == from ? edge.From : edge.To;
                    
                    if(dist[to] == int.MaxValue)
                    {
                        queue.Add(to);
                    }

                    var newDist = dist[from] + edge.Weight;
                    if (newDist < dist[to] || dist[to] == 0)
                    {
                        dist[to] = newDist;
                        queue = new SortedSet<int>(queue,
                            Comparer<int>.Create((f, s) => dist[f] - dist[s]));
                    }
                }
            }
            
            if(dist[startNode] != 0)
            {
                Console.WriteLine(dist[startNode]);
            }
            else
            {
                Console.WriteLine(dist.Where(d => d !=int.MaxValue).Count());
            }
        }

        private static void Initialize()
        {
            var nodesCount = int.Parse(Console.ReadLine());
            dist = new int[nodesCount];
            graph = new List<Edge>[nodesCount];
            for (int i = 0; i < nodesCount; i++)
            {
                dist[i] = int.MaxValue;
                graph[i] = new List<Edge>();
            }
        }

        private static void ReadGraph(int e)
        {
            for (int i = 0; i < e; i++)
            {
                var input = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

                var from = input[0];
                var to = input[1];
                var weight = input[2];

                var edge = new Edge(from, to, weight);
                graph[from].Add(edge);
            }
        }
    }
}
