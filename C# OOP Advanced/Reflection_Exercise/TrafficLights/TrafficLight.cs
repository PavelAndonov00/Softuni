namespace TrafficLights
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class TrafficLight
    {
        private Signal signal;

        public TrafficLight(string signal)
        {
            this.signal = Enum.Parse<Signal>(signal, true);
        }

        public void ChangeLight()
        {
            int current = (int)this.signal % 3 + 1;
            this.signal = (Signal)current;
        }


    }
}
