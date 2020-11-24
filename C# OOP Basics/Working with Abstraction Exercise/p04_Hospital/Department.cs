using System;
using System.Collections.Generic;
using System.Text;

class Department
{
    public string Type { get; set; }
    private int CurrentRoomIndex { get; set; }
    public List<Room> Rooms { get; set; }
    public bool IsFull { get; set; }

    public Department(string type)
    {
        Type = type;
        Rooms = new List<Room>();
        IsFull = false;

        InitializeRooms();
    }

    public void InitializeRooms()
    {
        for (int i = 0; i < 20; i++)
        {
            Rooms.Add(new Room());
        }
    }

    public void AccommodatePatient(Patient patient)
    {
        if (IsFull)
        {
            return;
        }

        var currentRoom = Rooms[CurrentRoomIndex];
        if(currentRoom.Beds == 0)
        {
            CurrentRoomIndex++;

            if(CurrentRoomIndex > 19)
            {
                IsFull = true;
                return;
            }
            currentRoom = Rooms[CurrentRoomIndex];
        }

        currentRoom.AddPatient(patient);
    }
}

