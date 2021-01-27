using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace Dijkstra
{

    class Program
    {
        class Edge
        {
            public int First { get; set; }

            public int Second { get; set; }

            public int Weight { get; set; }
        }

        static Dictionary<int, List<Edge>> nodesToEdges;
        static void Main(string[] args)
        {
            nodesToEdges = new Dictionary<int, List<Edge>>();
            ReadInput();
            var start = int.Parse(Console.ReadLine());
            var end = int.Parse(Console.ReadLine());

            var distances = new int[nodesToEdges.Keys.Max() + 1];
            distances[start] = 0;
            for (int i = 1; i < distances.Length; i++)
            {
                distances[i] = int.MaxValue;
            }

            var prev = new int[nodesToEdges.Keys.Max() + 1];
            var bag = new SortedSet<int>(Comparer<int>.Create((f, s) => distances[f] - distances[s]));
            bag.Add(start);
            while (bag.Count > 0)
            {
                var prevNode = bag.Min;
                bag.Remove(prevNode);

                foreach (var edge in nodesToEdges[prevNode])
                {
                    var currentNode = edge.First == prevNode
                        ? edge.Second
                        : edge.First;

                    if(distances[currentNode] == int.MaxValue)
                    {
                        bag.Add(currentNode);
                    }

                    var newDist = distances[prevNode] + edge.Weight;
                    if(distances[currentNode] > newDist)
                    {
                        distances[currentNode] = newDist;
                        prev[currentNode] = prevNode;
                        bag = new SortedSet<int>(bag, Comparer<int>.Create((f, s) => distances[f] - distances[s]));
                    }
                }
            }

            if(distances[end] == int.MaxValue)
            {
                Console.WriteLine("There is no such path.");
                return;
            }

            Console.WriteLine(distances[end]);
            var result = new Stack<int>();
            result.Push(end);
            var node = end;
            while (node != 0)
            {
                result.Push(prev[node]);
                node = prev[node];
            }
            Console.WriteLine(string.Join(" ", result));
        }

        private static void ReadInput()
        {
            var n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                var tokens = Console.ReadLine()
                    .Split(',');
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
            }
        }
    }
}
