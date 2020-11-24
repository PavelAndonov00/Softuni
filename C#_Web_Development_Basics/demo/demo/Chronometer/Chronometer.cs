using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace demo
{
    public class Chronometer : IChronometer
    {
        private long milliseconds;

        private List<string> laps;

        private bool isRunning;

        public Chronometer()
        {
            this.laps = new List<string>();
        }

        public string GetTime => $"{milliseconds/60000:D2}:{milliseconds/1000:D2}:{milliseconds:d4}";

        public string Laps => laps.Count == 0 ? "no laps" : string.Join("\n", laps);

        public string Lap()
        {
            var lap = this.GetTime;
            this.laps.Add(lap);
            return lap;
        }

        public void Reset()
        {
            this.Stop();
            this.milliseconds = 0;
        }

        public void Start()
        {
            this.isRunning = true;
            Task.Run(() =>
            {
                while (isRunning)
                {
                    Thread.Sleep(1);
                    this.milliseconds+=2;
                }
            });
        }

        public void Stop()
        {
            this.isRunning = false;
        }
    }
}
