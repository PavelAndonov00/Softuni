using System;
using System.Collections.Generic;
using System.Linq;

class StartUp
{
    static void Main(string[] args)
    {
        var n = int.Parse(Console.ReadLine());

        var employees = new Dictionary<string, List<Employee>>();
        for (int i = 0; i < n; i++)
        {
            var input = Console.ReadLine()
                .Split()
                .ToList();

            var name = input[0];
            var salary = decimal.Parse(input[1]);
            var position = input[2];
            var department = input[3];

            var email = "n/a";
            var age = -1;
            if (input.Count == 5)
            {
                if (input[4].Contains('@'))
                {
                    email = input[4];
                }
                else
                {
                    age = int.Parse(input[4]);
                }
            }
            else if (input.Count == 6)
            {
                email = input[4];
                age = int.Parse(input[5]);

            }

            if (!employees.ContainsKey(department))
            {
                employees[department] = new List<Employee>();
            }
            employees[department].Add(new Employee(name, salary, position, department, email, age));
        }

        var highestAvgSalaryDep = employees
            .OrderByDescending(x => x.Value.Average(e => e.Salary))
            .FirstOrDefault();

        Console.WriteLine($"Highest Average Salary: {highestAvgSalaryDep.Key}");
        var sortedEmployees = highestAvgSalaryDep.Value.OrderByDescending(x => x.Salary);
        foreach (var employee in sortedEmployees)
        {
            Console.WriteLine($"{employee.Name} {employee.Salary:f2} {employee.Email} {employee.Age}");
        }
    }
}
