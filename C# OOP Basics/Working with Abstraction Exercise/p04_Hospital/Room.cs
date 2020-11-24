using System;
using System.Collections.Generic;
using System.Text;

class Room
{
    public int Beds { get; set; }
    public List<Patient> Patients { get; set; }

    public Room()
    {
        Beds = 3;
        Patients = new List<Patient>(3);
    }

    public void AddPatient(Patient patient)
    {
        Patients.Add(patient);
        Beds--;
    }
}

