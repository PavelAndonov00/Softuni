using System;
using System.IO;

namespace p02_Line_Numbers
{
    class Program
    {
        static void Main(string[] args)
        {
            var pathForRead = "../../../ForRead.txt";
            var pathForWrite = "../../../ForWrite.txt";
            var reader = new StreamReader(pathForRead);
            var writer = new StreamWriter(pathForWrite);

            using (writer)
            {
                using (reader)
                {
                    var current = reader.ReadLine();
                    var counter = 1;
                    while (current != null)
                    {
                        writer.WriteLine($"Line {counter++}:" + current);

                        current = reader.ReadLine();
                    }

                }
            }

            Console.WriteLine("DONE");
            
        }
    }
}
