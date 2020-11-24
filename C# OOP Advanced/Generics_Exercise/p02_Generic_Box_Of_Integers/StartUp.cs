using p01_Generic_Box_of_String;
using System;
using System.Collections.Generic;

namespace p02_Generic_Box_Of_Integers
{
    class StartUp
    {
        static void Main(string[] args)
        {
            IList<Box<int>> boxes = new List<Box<int>>();

            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                int input = int.Parse(Console.ReadLine());

                boxes.Add(new Box<int>(input));
            }

            foreach (var box in boxes)
            {
                Console.WriteLine(box);
            }
        }
    }
}
