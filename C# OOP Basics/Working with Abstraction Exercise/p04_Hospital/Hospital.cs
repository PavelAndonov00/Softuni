using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Hospital
{
    public List<Department> Departments { get; set; }
    public List<Doctor> Doctors { get; set; }

    public Hospital()
    {
        Departments = new List<Department>();
        Doctors = new List<Doctor>();
    }

    private void AddDoctor(Doctor doctor)
    {
        Doctors
            .Add(doctor);
    }

    public void AddDepartment(Department department)
    {
        Departments
            .Add(department);
    }

    public void AccommodatePatientWithHealingDoctor(Patient patient, string department, Doctor doctor)
    {
        var currentDepartment = Departments
            .FirstOrDefault(d => d.Type == department);
        currentDepartment
            .AccommodatePatient(patient);

        if (!Doctors.Any(d => d.FirstName == doctor.FirstName))
        {
            AddDoctor(doctor);
        }

        Doctors
            .FirstOrDefault(d => d.FirstName == doctor.FirstName)
            .AddPatient(patient);
    }

    public string GetPatientsInDepartment(string department)
    {
        var currentDepartment = Departments
            .FirstOrDefault(d => d.Type == department);
        var notEmptyRooms = currentDepartment
            .Rooms
            .Where(r => r.Patients.Count > 0)
            .ToList();
        var patients = notEmptyRooms
            .Select(e => String.Join(Environment.NewLine, e.Patients.Select(p => p.Name))
            .ToString());
        return String
            .Join(Environment.NewLine, patients);
    }

    public string GetPatientsInDepartmentRoom(string department, int roomNumber)
    {
        var currentRoom = Departments
            .FirstOrDefault(d => d.Type == department)
            .Rooms[roomNumber - 1];

        return String
            .Join(Environment.NewLine, currentRoom.Patients.Select(p => p.Name)
            .OrderBy(p => p));
    }
}

