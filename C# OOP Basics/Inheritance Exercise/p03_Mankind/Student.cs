using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

class Student : Human
{
    private Regex facultyNumberRegex = new Regex(@"[A-Za-z0-9]{5,10}");
    private string facultyNumber;

    public Student(string firstName, string lastName, string facultyNumber)
        : base(firstName, lastName)
    {
        this.FacultyNumber = facultyNumber;
    }

    public string FacultyNumber
    {
        get
        {
            return this.facultyNumber;
        }

        protected set
        {
            if (!facultyNumberRegex.IsMatch(value))
            {
                throw new ArgumentException("Invalid faculty number!");
            }

            this.facultyNumber = value;
        }
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append(base.ToString());
        sb.AppendLine($"Faculty number: {this.FacultyNumber}");

        return sb.ToString();
    }
}
