using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace BlackFriday
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    namespace Kruskal
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
            static int[] parents;
            static int sum;
            static void Main(string[] args)
            {
                nodes = new HashSet<int>();
                edges = new List<Edge>();
                ReadInpit();

                parents = new int[nodes.Count];
                for (int i = 0; i < parents.Length; i++)
                {
                    parents[i] = i;
                }
                while (edges.Count > 0)
                {
                    var minEdge = edges[0];
                    edges.RemoveAt(0);

                    var firstRoot = FindRoot(minEdge.First);
                    var secondRoot = FindRoot(minEdge.Second);

                    if (firstRoot != secondRoot)
                    {
                        sum += minEdge.Weight;
                        parents[firstRoot] = secondRoot;
                    }
                }

                Console.WriteLine(sum);
            }

            private static int FindRoot(int first)
            {
                var parent = parents[first];
                while (parent != parents[parent])
                {
                    parent = parents[parent];
                }

                return parent;
            }

            private static void ReadInpit()
            {
                var numberOfEdges1 = int.Parse(Console.ReadLine());
                var numberOfEdges = int.Parse(Console.ReadLine());
                for (int i = 0; i < numberOfEdges; i++)
                {
                    var tokens = Console.ReadLine()
                        .Split();
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

                edges = edges.OrderBy(e => e.Weight).ToList();
            }
        }
    }


}
