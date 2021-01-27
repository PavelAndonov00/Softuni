using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace MostReliablePath
{
    class Program
    {
        class Edge
        {
            public int First { get; set; }

            public int Second { get; set; }

            public int Weight { get; set; }
        }

        static Dictionary<int, List<Edge>> nodesToEdges = new Dictionary<int, List<Edge>>();
        static HashSet<int> nodes = new HashSet<int>();
        static void Main(string[] args)
        {
            Readinput();

            var source = int.Parse(Console.ReadLine());
            var destination = int.Parse(Console.ReadLine());
            var distances = new double[nodes.Max() + 1];
            var prev = new int[nodes.Max() + 1];
            foreach (var node in nodes)
            {
                distances[node] = double.NegativeInfinity;
                prev[node] = -1;
            }

            var comparer = Comparer<int>.Create((f, s) => distances[s].CompareTo(distances[f]));
            distances[source] = 100;
            var priorityQueue = new OrderedBag<int>(comparer);
            priorityQueue.Add(source);
            while (priorityQueue.Count > 0)
            {
                var firstNode = priorityQueue.RemoveFirst();

                if(firstNode == destination)
                {
                    break;
                }

                foreach (var edge in nodesToEdges[firstNode])
                {
                    var secondNode = edge.Second == firstNode ? edge.First : edge.Second;
                    if (double.IsNegativeInfinity(distances[secondNode]))
                    {
                        priorityQueue.Add(secondNode);
                    }

                    var newDistance = distances[firstNode] * edge.Weight / 100;
                    if (newDistance > distances[secondNode])
                    {
                        distances[secondNode] = newDistance;
                        prev[secondNode] = firstNode;
                        priorityQueue = new OrderedBag<int>(priorityQueue, comparer);
                    }
                }
            }
            Console.WriteLine($"Most reliable path reliability: {distances[destination]:F2}%");
            var path = new Stack<int>();
            path.Push(destination);
            var currentNode = prev[destination];
            while (currentNode != -1)
            {
                path.Push(currentNode);
                currentNode = prev[currentNode];
            }
            Console.WriteLine(string.Join(" -> ", path));
        }

        private static void Readinput()
        {
            Console.ReadLine(); // number of nodes
            var numberOfEdges = int.Parse(Console.ReadLine());
            for (int i = 0; i < numberOfEdges; i++)
            {
                var tokens = Console.ReadLine()
                    .Split(' ');
                var first = int.Parse(tokens[0]);
                var second = int.Parse(tokens[1]);
                var weight = int.Parse(tokens[2]);

                if (!nodesToEdges.ContainsKey(first))
                {
                    nodesToEdges[first] = new List<Edge>();
                }

                if (!nodesToEdges.ContainsKey(second))
                {
                    nodesToEdges[second] = new List<Edge>();
                }

                var edge = new Edge
                {
                    First = first,
                    Second = second,
                    Weight = weight
                };
                nodesToEdges[first].Add(edge);
                nodesToEdges[second].Add(edge);
                nodes.Add(first);
                nodes.Add(second);
            }
        }
    }
}
