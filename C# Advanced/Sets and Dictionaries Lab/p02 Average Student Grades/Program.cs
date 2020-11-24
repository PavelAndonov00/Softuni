using System;
using System.Collections.Generic;
using System.Linq;

namespace p02_Average_Student_Grades
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());

            var result = new Dictionary<string, List<double>>();
            for (int i = 0; i < n; i++)
            {
                var input = Console.ReadLine()
                    .Split()
                    .ToArray();

                var student = input[0];
                var grade = double.Parse(input[1]);

                if (!result.ContainsKey(student))
                {
                    result[student] = new List<double>();
                }

                result[student].Add(grade);
            }

            foreach (var key in result.Keys)
            {
                var grades = result[key];
                var averageGrade = grades.Average();
                var resultString = $"{key} -> ";
                foreach (var grade in grades)
                {
                    resultString += $"{grade:f2} ";
                }
                Console.WriteLine(resultString + $"(avg: {averageGrade:f2})");
            }
        }
    }
}
