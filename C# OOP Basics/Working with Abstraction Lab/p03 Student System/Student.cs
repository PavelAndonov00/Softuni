using System;
using System.Collections.Generic;
using System.Text;

class Student
{
    private string name;
    private int age;
    private double grade;

    public double Grade
    {
        get { return grade; }
        set { grade = value; }
    }

    public int Age
    {
        get { return age; }
        set { age = value; }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public Student(string name, int age, double grade)
    {
        Name = name;
        Age = age;
        Grade = grade;
    }

    public string GetCommentary()
    {
        if (Grade >= 5.00)
        {
            return " Excellent student.";
        }
        else if (Grade < 5.00 && Grade >= 3.50)
        {
            return " Average student.";
        }
        else
        {
            return " Very nice person.";
        }
    }

    public override string ToString()
    {

        return $"{Name} is {Age} years old." + GetCommentary();
    }
}
