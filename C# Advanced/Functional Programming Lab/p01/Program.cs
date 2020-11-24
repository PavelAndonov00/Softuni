using System;
using System.Linq;

namespace p01
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(String.Join(", ", Console.ReadLine()
                .Split(", ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .Where(e => e % 2 == 0)
                .OrderBy(e => e)
                .ToArray()));
        }
    }
}
