﻿using System;
using System.Collections.Generic;
using System.Text;

class Parent
{
    public string name { get; set; }
    public string birthday { get; set; }

    public Parent(string name, string birthday)
    {
        this.name = name;
        this.birthday = birthday;
    }

    public override string ToString()
    {
        if (this.name != null && this.birthday != null)
        {
            return $"{this.name} {this.birthday}";
        }

        return "";
    }
}

