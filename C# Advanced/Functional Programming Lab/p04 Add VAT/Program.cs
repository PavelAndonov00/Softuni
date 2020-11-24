using System;
using System.Linq;

namespace p04_Add_VAT
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine()
                .Split(", ", StringSplitOptions.RemoveEmptyEntries)
                .Select(e => $"{double.Parse(e) * 1.20:f2}")
                .ToList()
                .ForEach(Console.WriteLine);

        }
    }
}
