using System;
using System.Collections.Generic;
using System.Linq;

namespace p05_Count_Symbols
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine()
                .ToCharArray();

            var chars = new Dictionary<char, int>();
            foreach (var ch in input)
            {
                if(!chars.ContainsKey(ch))
                {
                    chars[ch] = 0;
                }

                chars[ch]++;
            }

            foreach (var ch in chars.OrderBy(e => e.Key))
            {
                Console.WriteLine(ch.Key + ": " + ch.Value + " time/s");
            }
        }
    }
}
