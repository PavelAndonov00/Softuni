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
            Assemble(paths, copyPath + "Part5.mp4");

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
                        using (var gz = new GZipStream(reader, CompressionMode.Decompress))
                        {
                            var buffer = new byte[1024];
                            while (true)
                            {
                                var readBytes = gz.Read(buffer, 0, buffer.Length);

                                if (readBytes == 0) break;

                                writer.Write(buffer, 0, readBytes);
                            }
                        }
                    }                  
                }
            }
        }

        static void Slice(string sourceFile, string destinationDirectory, int parts)
        {

            using (var reader = new FileStream(sourceFile, FileMode.Open))
            {
                var slicingLength = reader.Length / parts - 10 * 100;                
                var total = 0;
                var readBytes = 0;
                var buffer = new byte[] { };
                for (int i = 0; i < parts - 1; i++)
                {
                    var dest = destinationDirectory + $"Part {i + 1}.mp4.gz";
                    paths.Add(dest);
                    buffer = new byte[slicingLength];
                    readBytes = reader.Read(buffer, 0, buffer.Length);
                    total += readBytes;
                    using (var writer = new FileStream(dest, FileMode.Create))
                    using (var gzipper = new GZipStream(writer, CompressionMode.Compress))
                    {
                        gzipper.Write(buffer, 0, buffer.Length);
                    }
                }

                buffer = new byte[reader.Length - total];
                readBytes = reader.Read(buffer, 0, buffer.Length);
                var lastDest = destinationDirectory + $"Part {parts}.mp4.gz";
                paths.Add(lastDest);
                using (var writer = new FileStream(lastDest, FileMode.Create))
                using (var gzipper = new GZipStream(writer, CompressionMode.Compress))
                {
                    gzipper.Write(buffer, 0, buffer.Length);
                }
            }


        }

    }
}
