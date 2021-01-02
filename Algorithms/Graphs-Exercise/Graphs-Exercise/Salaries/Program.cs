using System;
using System.Collections.Generic;
using System.Linq;

namespace Salaries
{
    class Program
    {
        static List<int>[] graph;
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            graph = new List<int>[n];
            PopulateGraph(n);

            var total = 0;
            for (int i = 0; i < graph.Length; i++)
            {
                total += GetSalary(i);
            }
            Console.WriteLine(total);
        }

        private static int GetSalary(int currentNode)
        {
            if (graph[currentNode].Count == 0)
            {
                return 1;
            }

            var sum = 0;
            foreach (var child in graph[currentNode])
            {
                sum += GetSalary(child);
            }

            return sum;
        }

        private static void PopulateGraph(int n)
        {
            for (int i = 0; i < n; i++)
            {
                if (graph[i] == null)
                {
                    graph[i] = new List<int>();
                }

                var tokens = Console.ReadLine().ToCharArray();
                for (int k = 0; k < n; k++)
                {
                    if (tokens[k] == 'Y')
                    {
                        graph[i].Add(k);
                    }
                }
            }
        }
    }
}
