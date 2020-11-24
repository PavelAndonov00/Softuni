using System;
using System.Linq;

namespace p03_Count_Uppercase_Words
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Where(e => Char.IsUpper(e[0]))
                .ToList()
                .ForEach(Console.WriteLine);
        }
    }
}
