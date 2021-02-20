using System;
using System.Collections.Generic;
using System.Linq;

namespace EmergencyPlan
{
    class Program
    {
        class Edge
        {
            public int First { get; set; }

            public int Second { get; set; }

            public TimeSpan Weight { get; set; }
        }
        static List<Edge>[] nodesToEdges;
        static int[] exits;
        static TimeSpan[] distances;
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            exits = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();
            nodesToEdges = new List<Edge>[n];
            for (int i = 0; i < n; i++)
            {
                nodesToEdges[i] = new List<Edge>();
            }
            ReadInput();
            var timeTokens = Console.ReadLine().Split(':');
            var mins = int.Parse(timeTokens[0]);
            var secs = int.Parse(timeTokens[1]);
            var time = new TimeSpan(0, mins, secs);
            for (int i = 0; i < n; i++)
            {
                if (!exits.Contains(i))
                {
                    Dijkstra(i);

                    SafeOrUnsafe(i, time);
                }
            }
        }

        private static void SafeOrUnsafe(int room, TimeSpan time)
        {
            var min = TimeSpan.MaxValue;
            foreach (var exit in exits)
            {
                if (distances[exit] < min)
                {
                    min = distances[exit];
                }
            }

            if(min == TimeSpan.MaxValue)
            {
                Console.WriteLine($"Unreachable {room} (N/A)");
            }
            else if(min <= time)
            {
                Console.WriteLine($"Safe {room} ({min})");
            }
            else
            {
                Console.WriteLine($"Unsafe {room} ({min})");
            }
        }

        private static void Dijkstra(int start)
        {
            distances = new TimeSpan[nodesToEdges.Length];
            for (int i = 0; i < distances.Length; i++)
            {
                distances[i] = TimeSpan.MaxValue;
            }
            distances[start] = new TimeSpan();

            var bag = new SortedSet<int>(Comparer<int>.Create((f, s) => distances[f].CompareTo(distances[s])));
            bag.Add(start);
            while (bag.Count > 0)
            {
                var prevNode = bag.Min;
                bag.Remove(prevNode);

                if (exits.Contains(prevNode))
                {
                    break;
                }

                foreach (var edge in nodesToEdges[prevNode])
                {
                    var currentNode = edge.First == prevNode
                        ? edge.Second
                        : edge.First;

                    if (distances[currentNode] == TimeSpan.MaxValue)
                    {
                        bag.Add(currentNode);
                    }

                    var newDist = distances[prevNode] + edge.Weight;
                    if (distances[currentNode] > newDist)
                    {
                        distances[currentNode] = newDist;
                        bag = new SortedSet<int>(bag, Comparer<int>.Create((f, s) => distances[f].CompareTo(distances[s])));
                    }
                }
            }
        }

        private static void ReadInput()
        {
            var c = int.Parse(Console.ReadLine());
            for (int i = 0; i < c; i++)
            {
                var tokens = Console.ReadLine()
                    .Split();
                var first = int.Parse(tokens[0]);
                var second = int.Parse(tokens[1]);
                var timeTokens = tokens[2].Split(':');
                var mins = int.Parse(timeTokens[0]);
                var secs = int.Parse(timeTokens[1]);
                var weight = new TimeSpan(0, mins, secs);

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
