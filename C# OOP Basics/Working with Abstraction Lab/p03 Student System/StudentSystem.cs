using System;
using System.Collections.Generic;
using System.Linq;

class StudentSystem
{
    private Dictionary<string, Student> repo;

    public StudentSystem()
    {
        this.repo = new Dictionary<string, Student>();
    }

    public void ParseCommand(string input)
    {
        var args = input
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .ToArray();

        switch (args[0])
        {
            case "Create":
                CreateStudent(args[1], int.Parse(args[2]), double.Parse(args[3]));
                break;
            case "Show":
                ShowStudent(args[1]);
                break;
        }
    }

    private void ShowStudent(string name)
    {
        if (repo.ContainsKey(name))
        {
            var student = repo[name];
            
            Console.WriteLine(student.ToString());
        }
    }

    private void CreateStudent(string name, int age, double grade)
    {
        if (!repo.ContainsKey(name))
        {
            var student = new Student(name, age, grade);
            repo[name] = student;
        }
    }
}


