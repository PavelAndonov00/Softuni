using System;
using System.IO;

namespace p01_Odd_Lines
{
    class ForRead
    {
        static void Main(string[] args)
        {
            var path = "../../../ForRead.txt";
            var reader = new StreamReader(path);

            var counter = 0;
            using (reader)
            {
                while (true)
                {
                    var current = reader.ReadLine();
                    if (counter++ % 2 != 0)
                    {
                        Console.WriteLine(current);
                        break;
                    }
                }                
            }
           
        }
    }
}
