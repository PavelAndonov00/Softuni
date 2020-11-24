using System;
using System.Collections.Generic;
using System.Linq;

public class StartUp
{
    public static void Main()
    {
        var hospital = new Hospital();

        string command = Console.ReadLine();
        while (command != "Output")
        {
            string[] tokens = command.Split();
            var department = tokens[0];
            var doctorFirstName = tokens[1];
            var doctorSecondName = tokens[2];
            var patientName = tokens[3];

            if (!hospital.Departments.Any(d => d.Type == department))
            {
                hospital.AddDepartment(new Department(department));
            }

            var patient = new Patient(patientName);
            var doctor = new Doctor(doctorFirstName, doctorSecondName);
            hospital.AccommodatePatientWithHealingDoctor(patient, department, doctor);

            command = Console.ReadLine();
        }

        command = Console.ReadLine();
        while (command != "End")
        {
            string[] args = command.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            if (args.Length == 1)
            {
                Console.WriteLine(hospital.GetPatientsInDepartment(args[0]));
            }
            else if (args.Length == 2 && int.TryParse(args[1], out var room))
            {
                Console.WriteLine(hospital.GetPatientsInDepartmentRoom(args[0], room));
            }
            else
            {
                var currentDoctor = hospital.Doctors.FirstOrDefault(d => d.FirstName == args[0]);
                Console.WriteLine(currentDoctor.GetPatiens());
            }
            command = Console.ReadLine();
        }
    }
}

