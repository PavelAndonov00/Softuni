using System;
using System.Collections.Generic;

namespace p06_Generic_Count_Method_Double
{
    class StartUp
    {
        static void Main(string[] args)
        {
            IList<Box<double>> boxes = new List<Box<double>>();

            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                double input = Double.Parse(Console.ReadLine());

                boxes.Add(new Box<double>(input));
            }

            string element = Console.ReadLine();
            int counter = 0;
            for (int i = 0; i < boxes.Count; i++)
            {
                if (boxes[i].CompareTo(Double.Parse(element)) == 1)
                {
                    counter++;
                }
            }

            Console.WriteLine(counter);
        }
    }
}
