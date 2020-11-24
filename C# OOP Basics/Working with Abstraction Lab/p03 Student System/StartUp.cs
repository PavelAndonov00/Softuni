using System;
using System.Collections.Generic;
using System.Text;

class StartUp
{
    static void Main(string[] args)
    {
        StudentSystem studentSystem = new StudentSystem();
        var input = String.Empty;
        while ((input = Console.ReadLine()) != "Exit")
        {
            studentSystem.ParseCommand(input);
        }
    }
}

