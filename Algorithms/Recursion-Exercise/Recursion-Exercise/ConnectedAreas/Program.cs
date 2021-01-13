using System;
using System.Collections.Generic;
using System.Linq;

namespace ConnectedAreas
{
    class Program
    {
        static char[,] area;
        static bool[,] visited;
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var m = int.Parse(Console.ReadLine());

            visited = new bool[n, m];
            area = new char[n, m];
            for (int r = 0; r < n; r++)
            {
                var row = Console.ReadLine();
                for (int c = 0; c < m; c++)
                {
                    area[r, c] = row[c];
                }
            }

            var areas = new Dictionary<KeyValuePair<int, int>, int>();
            for (int r = 0; r < area.GetLength(0); r++)
            {
                for (int c = 0; c < area.GetLength(1); c++)
                {
                    if(area[r, c] == '-' && !visited[r, c])
                    {
                        var size = 0;
                        GetSize(r, c, ref size);
                        var kvp = new KeyValuePair<int, int>(r, c);
                        areas[kvp] = size;
                    }
                }
            }
            var sorted = areas.OrderByDescending(a => a.Value)
                .ThenBy(a => a.Key.Key)
                .ThenBy(a => a.Key.Value);
            var counter = 1;
            Console.WriteLine("Total areas found: " + sorted.Count());
            foreach (var kvp in sorted)
            {
                var coordinates = kvp.Key;
                Console.WriteLine($"Area #{counter++} at ({coordinates.Key}, {coordinates.Value}), size: {kvp.Value}");
            }
        }

        private static void GetSize(int r, int c, ref int size)
        {
            if(r < 0 || c < 0 || 
               r > area.GetLength(0) - 1 ||
               c > area.GetLength(1) - 1 ||
               area[r, c] == '*' ||
               visited[r, c])
            {
                return;
            }

            visited[r, c] = true;
            size++;

            GetSize(r, c + 1, ref size);
            GetSize(r + 1, c, ref size);
            GetSize(r, c - 1, ref size);
            GetSize(r - 1, c, ref size);
        }
    }
}
