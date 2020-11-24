using System;
using System.Collections.Generic;

namespace p01_Generic_Box_of_String
{
    class StartUp
    {
        static void Main(string[] args)
        {
            IList<Box<string>> boxes = new List<Box<string>>();

            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                string input = Console.ReadLine();

                boxes.Add(new Box<string>(input));
            }

            foreach (var box in boxes)
            {
                Console.WriteLine(box);
            }
        }
    }
}
