using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace p05_Slicing_File
{
    class Program
    {
        static List<string> paths;

        static void Main(string[] args)
        {
            paths = new List<string>();
            var readerPath = "../../../../files/sliceMe.mp4";
            var copyPath = "../../../../files/";

            Slice(readerPath, copyPath, 4);
            Assemble(paths, copyPath + "assembler.mp4");

            Console.WriteLine("Done");
        }

        static void Assemble(List<string> files, string destinationDirectory)
        {
            using (var writer = new FileStream(destinationDirectory, FileMode.Create))            
            {
                foreach (var file in files)
                {
                    using (var reader = new FileStream(file, FileMode.Open))
                    {
                        var buffer = new byte[reader.Length];
                        reader.Read(buffer, 0, buffer.Length);
                        writer.Write(buffer, 0, buffer.Length);
                    }
                }
            }
        }

        static void Slice(string sourceFile, string destinationDirectory, int parts)
        {
            using (var reader = new FileStream(sourceFile, FileMode.Open))
            {
                var size = reader.Length;
                var currentPartLength = size / parts;

                var counter = 0;
                for (int i = 1; i < parts; i++)
                {
                    var dest = destinationDirectory + counter++ + ".mp4";
                    paths.Add(dest);
                    using (var writer = new FileStream(dest, FileMode.Create))
                    {
                        var buffer = new byte[4096];
                        var readBytes = 0;
                        var curr = currentPartLength;
                        while (curr >= 0)
                        {
                            readBytes = reader.Read(buffer, 0, buffer.Length);

                            curr -= readBytes;

                            writer.Write(buffer, 0, readBytes);
                        }
                    }
                }

                var lastDest = destinationDirectory + counter++ +".mp4";
                paths.Add(lastDest);
                using (var writer = new FileStream(lastDest, FileMode.Create))
                {
                    var buffer = new byte[4096];
                    var readBytes = 1;
                    while (readBytes > 0)
                    {
                        readBytes = reader.Read(buffer, 0, buffer.Length);                        

                        writer.Write(buffer, 0, readBytes);
                    }
                }
            }
            
        }
       
    }
}
