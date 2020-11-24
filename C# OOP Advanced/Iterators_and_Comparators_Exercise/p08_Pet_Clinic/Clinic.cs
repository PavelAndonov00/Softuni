namespace p08_Pet_Clinic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Clinic
    {
        private int centerIndex;

        public Clinic(string name, int roomsCount)
        {
            this.Name = name;
            this.Rooms = new List<Pet>();
            InitializeRooms(roomsCount);
            this.centerIndex = roomsCount / 2;
        }

        private void InitializeRooms(int roomsCount)
        {
            if (roomsCount % 2 == 0)
            {
                throw new InvalidOperationException("Invalid Operation!");
            }

            for (int i = 0; i < roomsCount; i++)
            {
                this.Rooms.Add(null);
            }
        }

        public string Name { get; }

        public List<Pet> Rooms { get; }

        public bool Add(Pet pet)
        {
            if(pet == null)
            {                
                throw new InvalidOperationException("Invalid Operation!");
            }

            for (int i = 0; i <= centerIndex; i++)
            {
                if(Rooms[centerIndex - i] == null)
                {
                    Rooms[centerIndex - i] = pet;
                    return true;
                }
                else if(Rooms[centerIndex + i] == null)
                {
                    Rooms[centerIndex + i] = pet;
                    return true;
                }
            }

            return false;
        }

        public bool Release()
        {
            for (int i = centerIndex; i < Rooms.Count; i++)
            {
                Pet currentPet = Rooms[i];
                if (currentPet != null)
                {
                    Rooms[i] = null;
                    return true;
                }
            }

            for (int i = 0; i < centerIndex; i++)
            {
                Pet currentPet = Rooms[i];
                if (currentPet != null)
                {
                    Rooms[i] = null;
                    return true;
                }
            }

            return false;
        }

        public bool HasEmptyRooms()
        {
            return this.Rooms.Where(r => r == null).Count() > 0;
        }

        public string Print()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var room in Rooms)
            {
                if(room == null)
                {
                    sb.AppendLine("Room empty");
                }
                else
                {
                    sb.AppendLine($"{room.Name} {room.Age} {room.Kind}");
                }
            }

            return sb.ToString().Trim();
        }

        public string Print(int roomNumber)
        {
            Pet currentRoom = Rooms.Where((r, n) => n + 1 == roomNumber).ToArray()[0];

            if (currentRoom == null)
            {
                return "Room empty";
            }
            else
            {
                return $"{currentRoom.Name} {currentRoom.Age} {currentRoom.Kind}";
            }
        }
    }
}
