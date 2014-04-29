using System;

namespace FerretLib.SFML
{
    public class ChronoEventArgs : EventArgs
    {
        public ChronoEventArgs(long monotonic, double delta, double fps)
        {
            Monotonic = monotonic;
            Delta = delta;
            DateTime = DateTime.Now;
            Fps = fps;
        }

        public long Monotonic { get; private set; }
        public double Delta { get; private set; }
        public double Fps { get; private set; }
        public DateTime DateTime { get; private set; }
    }
}