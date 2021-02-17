using System;
using System.Collections.Generic;
using System.Linq;

namespace ChainingLighting
{

    class Program
    {

        class Edge
        {
            public Edge(int from, int to, int weight, int damage)
            {
                this.From = from;
                this.To = to;
                this.Weight = weight;
                this.Damage = damage;
            }

            public int From { get; }
            public int To { get; }
            public int Weight { get; }
            public int Damage { get; set; }
        }

        static List<Edge>[] edges;
        static int[] damages;
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            Initialize(n);
            var m = int.Parse(Console.ReadLine());
            var l = int.Parse(Console.ReadLine());
            ReadGraph(m);
            for (int i = 0; i < l; i++)
            {
                var input = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();
                var node = input[0];
                var damage = input[1];
                Prim(n, node, damage);
            }

            Console.WriteLine(damages.Max());
        }

        private static void Initialize(int n)
        {
            edges = new List<Edge>[n];
            damages = new int[n];
            for (int i = 0; i < n; i++)
            {
                edges[i] = new List<Edge>();
            }
        }

        private static void Prim(int n, int node, int damage)
        {
            damages[node] += damage;
            var spanningTree = new bool[n];
            var queue = new SortedSet<Edge>(Comparer<Edge>.Create((f, s) => f.Weight - s.Weight));
            foreach (var edge in edges[node])
            {
                var newEdge = new Edge(edge.From, edge.To, edge.Weight, edge.Damage);
                newEdge.Damage = damage / 2;
                queue.Add(newEdge);
            }
            spanningTree[node] = true;
            while (queue.Count > 0)
            {
                var minNode = queue.First();
                queue.Remove(minNode);

                var first = minNode.From;
                var second = minNode.To;

                var nonTree = -1;
                if (spanningTree[first] && !spanningTree[second])
                {
                    nonTree = second;
                }
                else if (!spanningTree[first] && spanningTree[second])
                {
                    nonTree = first;
                }

                if (nonTree != -1)
                {
                    spanningTree[nonTree] = true;
                    damages[nonTree] += minNode.Damage;
                    foreach (var edge in edges[nonTree])
                    {
                        var newEdge = new Edge(edge.From, edge.To, edge.Weight, edge.Damage);
                        newEdge.Damage = minNode.Damage / 2;
                        queue.Add(newEdge);
                    }
                }
            }
        }

        private static void ReadGraph(int m)
        {
            for (int i = 0; i < m; i++)
            {
                var input = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

                var from = input[0];
                var to = input[1];
                var weight = input[2];

                var edge = new Edge(from, to, weight, 0);
                edges[from].Add(edge);
                edges[to].Add(edge);
            }
        }
    }
}
