using System;
using System.Collections.Generic;
using System.Text;

namespace demo
{
    public interface IChronometer
    {
        string GetTime { get; }

        string Laps { get; }

        void Start();

        void Stop();

        string Lap();

        void Reset();
    }
}
