using System;
using System.Collections.Generic;

namespace p01_Unique_Usernames
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var result = new HashSet<string>();
            for (int i = 0; i < n; i++)
            {
                result.Add(Console.ReadLine());
            }

            foreach (var name in result)
            {
                Console.WriteLine(name);
            }
        }
    }
}
