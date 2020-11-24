using System;
using System.Collections.Generic;
using System.Text;

class Company
{
    public string name { get; set; }
    public string department { get; set; }
    public decimal salary { get; set; }

    public Company()
    {

    }

    public Company(string name, string department, decimal salary) : this()
    {
        this.name = name;
        this.department = department;
        this.salary = salary;
    }

    public override string ToString()
    {
        var result = "Company:\n";
        if (this.name != null && this.department != null && this.salary != 0)
        {
            result += $"{this.name} {this.department} {this.salary:f2}\n";
        }

        return result;
    }
}

