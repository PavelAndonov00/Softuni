using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace p03_Word_Count
{
    class Program
    {
        static void Main(string[] args)
        {
            var basePath = "../../../";
            var wordsPath = basePath + "words.txt";
            var textPath = basePath + "text.txt";
            var resultPath = basePath + "result.txt";

            using (var words = new StreamReader(wordsPath))
            {
                var wordsList = words.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                var counter = new Dictionary<string, int>();
                using (var text = new StreamReader(textPath))
                {
                    using (var result = new StreamWriter(resultPath))
                    {
                        var currentLine = text.ReadLine();
                        while (currentLine != null)
                        {
                            var currentArr = currentLine
                                .Split(new char[] { '.', ',', ':', '!', '?', ' ', '-'}, StringSplitOptions.RemoveEmptyEntries)
                                .ToArray();

                            foreach (var w in currentArr)
                            {
                                if (wordsList.Contains(w.ToLower()))
                                {
                                    if(!counter.ContainsKey(w.ToLower()))
                                    {
                                        counter[w.ToLower()] = 0;
                                    }
                                    counter[w.ToLower()]++;
                                }
                            }

                            currentLine = text.ReadLine();
                        }

                        foreach (var key in counter.OrderByDescending(x => x.Value))
                        {
                            result.WriteLine($"{key.Key} - {key.Value}");
                        }

                    }
                }

                Console.WriteLine("DOne");
            }
        }
    }
}
