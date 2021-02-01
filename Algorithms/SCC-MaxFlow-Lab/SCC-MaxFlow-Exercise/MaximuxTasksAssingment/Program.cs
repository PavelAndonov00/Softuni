using System;
using System.Collections.Generic;

namespace MaximuxTasksAssingment
{
    class Program
    {
        static int[][] graph;
        static int[] parents;
        // Counts
        static int peopleCount;
        static int tasksCount;
        static int graphLength;
        static int source;
        static int destination;
        static void Main(string[] args)
        {
            peopleCount = int.Parse(Console.ReadLine());
            tasksCount = int.Parse(Console.ReadLine());
            graphLength = peopleCount + tasksCount + 2;
            source = 0;
            destination = graphLength - 1;
            graph = new int[graphLength][];
            ReadGraph();

            parents = new int[graphLength];
            for (int index = 0; index < graphLength; index++)
            {
                parents[index] = -1;
            }
            while (BFS())
            {
                for (int i = 0; i < graphLength; i++)
                {
                    Console.WriteLine(string.Join(" ", graph[i]));
                }
                Console.WriteLine(new string('*', 200));

                var to = destination;
                var from = parents[to];
                while (to != -1 && from != -1)
                {
                    graph[from][to]--;
                    graph[to][from] = 1;

                    to = from;
                    from = parents[to];
                }
            }

            for (int i = 0; i < graphLength; i++)
            {
                Console.WriteLine(string.Join(" ", graph[i]));
            }
            Console.WriteLine(new string('*', 200));

            var result = new Stack<string>();
            for (int i = peopleCount + 1; i <= peopleCount + tasksCount; i++)
            {
                for (int k = 0; k < graphLength; k++)
                {
                    if (graph[i][k] == 1)
                    {
                        result.Push((char)(64 + k) + "-" + (i - tasksCount));
                        break;
                    }
                }
            }

            Console.WriteLine(string.Join("\r\n", result));
        }

        private static bool BFS()
        {
            var visited = new bool[graph.Length];
            visited[source] = true;
            var queue = new Queue<int>();
            queue.Enqueue(source);
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if(current == destination)
                {
                    return true;
                }

                for (int task = 0; task < graphLength; task++)
                {
                    if(!visited[task] &&
                        graph[current][task] > 0)
                    {
                        queue.Enqueue(task);
                        visited[task] = true;
                        parents[task] = current;
                    }
                }
            }

            return false;
        }

        private static void ReadGraph()
        {
            // Assign tasks to people
            for (int personIndex = 1; personIndex <= tasksCount; personIndex++)
            {
                var tasks = Console.ReadLine();
                graph[personIndex] = new int[graphLength];
                for (int taskIndex = 0; taskIndex < tasks.Length; taskIndex++)
                {
                    if(tasks[taskIndex] == 'Y')
                    {
                        graph[personIndex][taskIndex + 1 + tasksCount] = 1;
                    }
                }
            }

            // Assign people to source
            graph[source] = new int[graphLength];
            for (int personIndex = 1; personIndex <= peopleCount; personIndex++)
            {
                graph[source][personIndex] = 1;
            }

            // Assign destination to tasks
            for (int taskIndex = 1; taskIndex <= tasksCount; taskIndex++)
            {
                graph[peopleCount + taskIndex] = new int[graphLength];
                graph[peopleCount + taskIndex][destination] = 1;
            }

            //Initialize destination
            graph[destination] = new int[graphLength];
        }
    }
}
