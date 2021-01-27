using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace Prim
{
    class Program
    {
        class Edge
        {
            public int First { get; set; }

            public int Second { get; set; }

            public int Weight { get; set; }
        }

        static HashSet<int> nodes;
        static List<Edge> edges;
        static HashSet<int> spanningTreeNodes;
        static void Main(string[] args)
        {
            nodes = new HashSet<int>();
            edges = new List<Edge>();
            ReadInpit();

            spanningTreeNodes = new HashSet<int>();
            foreach (var node in nodes)
            {
                if (!spanningTreeNodes.Contains(node))
                {
                    Prim(node);
                }
            }
        }

        private static void Prim(int node)
        {
            spanningTreeNodes.Add(node);
            var priorityQueue = new OrderedBag<Edge>(
                Comparer<Edge>.Create((f, s) => f.Weight - s.Weight));
            priorityQueue.AddMany(edges.Where(e => e.First == node || e.Second == node));
            while (priorityQueue.Count > 0)
            {
                var minEdge = priorityQueue.First();
                priorityQueue.Remove(minEdge);
                var firstNode = minEdge.First;
                var secondNode = minEdge.Second;

                int nonTreeNode = -1;
                if (spanningTreeNodes.Contains(firstNode) && !spanningTreeNodes.Contains(secondNode))
                {
                    nonTreeNode = secondNode;
                }
                else if (!spanningTreeNodes.Contains(firstNode) && spanningTreeNodes.Contains(secondNode))
                {
                    nonTreeNode = firstNode;
                }

                if (nonTreeNode != -1)
                {
                    spanningTreeNodes.Add(nonTreeNode);
                    priorityQueue.AddMany(edges.Where(e => e.First == nonTreeNode || e.Second == nonTreeNode));
                    Console.WriteLine($"{firstNode} - {secondNode}");
                }
            }
        }

        private static void ReadInpit()
        {
            var numberOfEdges = int.Parse(Console.ReadLine());
            for (int i = 0; i < numberOfEdges; i++)
            {
                var tokens = Console.ReadLine()
                    .Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var first = int.Parse(tokens[0]);
                var second = int.Parse(tokens[1]);
                var weight = int.Parse(tokens[2]);

                var edge = new Edge
                {
                    First = first,
                    Second = second,
                    Weight = weight
                };
                edges.Add(edge);
                nodes.Add(first);
                nodes.Add(second);
            }
        }
    }
}
