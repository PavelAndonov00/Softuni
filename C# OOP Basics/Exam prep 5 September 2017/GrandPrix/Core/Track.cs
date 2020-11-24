using System;
using System.Collections.Generic;
using System.Text;

public class Track
{
    public int Length { get; set; }
    public int TotalLaps { get; set; }
    public int CurrentLap { get; set; }
    public int RemainingLaps => this.TotalLaps - this.CurrentLap;

    public Track(int numberOfLaps, int trackLength)
    {
        this.TotalLaps = numberOfLaps;
        this.Length = trackLength;
    }


}
