namespace p03_Generic_Swap_Method_String
{
    using p01_Generic_Box_of_String;
    using System;
    using System.Collections.Generic;

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

            string[] indexes = Console.ReadLine().Split();
            int firstIndex = int.Parse(indexes[0]);
            int secondIndex = int.Parse(indexes[1]);

            Swap(boxes, firstIndex, secondIndex);

            foreach (var box in boxes)
            {
                Console.WriteLine(box);
            }
        }

        private static void Swap<T>(IList<Box<T>> list, int firstIndex, int secondIndex)
        {
            Box<T> firstItem = list[firstIndex];
            list[firstIndex] = list[secondIndex];
            list[secondIndex] = firstItem;
        }
    }
}
