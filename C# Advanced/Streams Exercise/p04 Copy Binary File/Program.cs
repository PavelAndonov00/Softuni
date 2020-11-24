using System;
using System.IO;

namespace p04_Copy_Binary_File
{
    class Program
    {
        static void Main(string[] args)
        {
            var filesPath = "../../../../files/copyMe.png";
            var copyFilePath = "../../../../files/copyMe-copy.png";

            using (var fileReader = new FileStream(filesPath, FileMode.Open))
            {
                var buffer = new byte[fileReader.Length];
                fileReader.Read(buffer, 0, buffer.Length);

                using (var fileCopier = new FileStream(copyFilePath, FileMode.Create))
                {
                    fileCopier.Write(buffer, 0, buffer.Length);
                }
            }

            Console.WriteLine("Done");
        }
    }
}
