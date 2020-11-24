namespace p05_Generic_Count_Method_Strings
{
    using System;
    using System.Collections.Generic;

    public class StartUp
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

            string element = Console.ReadLine();
            int counter = 0;
            for (int i = 0; i < boxes.Count; i++)
            {
                if (boxes[i].CompareTo(element) == 1)
                {
                    counter++;
                }                   
            }

            Console.WriteLine(counter);
        }
    }
}
