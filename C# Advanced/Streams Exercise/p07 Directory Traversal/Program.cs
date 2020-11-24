using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace p07_Directory_Traversal
{
    class Program
    {
        static void Main(string[] args)
        {
            var dir = "./";
            var files = Directory.GetFiles(dir);
            var dic = new Dictionary<string, Dictionary<string, double>>();
            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                var fileExtension = fileInfo.Extension;
                if (!dic.ContainsKey(fileExtension))
                {
                    dic[fileExtension] = new Dictionary<string, double>();
                }

                dic[fileExtension][fileInfo.Name] = fileInfo.Length;
            }

            using (var writer = new StreamWriter("../../../report.txt"))
            {
                foreach (var firstDic in dic.OrderByDescending(x => x.Value.Count).ThenBy(x => x.Key))
                {
                    writer.WriteLine(firstDic.Key);
                    foreach (var secondDic in firstDic.Value.OrderBy(x => x.Value))
                    {
                        writer.WriteLine($"--{secondDic.Key} - {secondDic.Value}kb");
                    }
                }

            }
        }
    }
}
