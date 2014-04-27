using System;

namespace FerretLib.SFML
{
    public class ChronoEventArgs : EventArgs
    {
        public ChronoEventArgs(int monotonic, float delta, float fps)
        {
            Monotonic = monotonic;
            Delta = delta;
            Fps = fps;
        }

        public int Monotonic { get; private set; }
        public float Delta { get; private set; }
        public float Fps { get; private set; }
    }
}