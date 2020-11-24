using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Doctor
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<Patient> Patients { get; set; }

    public Doctor()
    {
        Patients = new List<Patient>();
    }

    public Doctor(string firstName, string lastName) : this()
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public void AddPatient(Patient patient)
    {
        Patients.Add(patient);
    }

    public string GetPatiens()
    {
        return String.Join(Environment.NewLine, Patients.Select(p => p.Name).OrderBy(p => p));
    }
}

