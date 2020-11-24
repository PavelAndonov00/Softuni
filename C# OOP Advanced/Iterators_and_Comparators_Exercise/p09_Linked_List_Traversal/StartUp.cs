namespace p09_Linked_List_Traversal
{
    using System;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            LinkedList<int> linkedList = new LinkedList<int>();
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                string[] tokens = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string command = tokens[0];
                switch (command)
                {
                    case "Add":
                        linkedList.Add(int.Parse(tokens[1]));
                        break;
                    case "Remove":
                        linkedList.Remove(int.Parse(tokens[1]));
                        break;
                }
            }

            Console.WriteLine(linkedList.Count);
            Console.WriteLine(string.Join(" ", linkedList));
        }
    }
}
