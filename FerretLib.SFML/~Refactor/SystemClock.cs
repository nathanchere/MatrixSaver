using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FerretLib.SFML
{
    /// <summary>
    /// TODO: move to appropriate FerretLib
    /// </summary>
    internal class HighPerformanceTimer
    {
        [DllImport("Kernel32.dll")]
        protected static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

        [DllImport("Kernel32.dll")]
        protected static extern bool QueryPerformanceFrequency(out long lpFrequency);

        protected long _monotonic;
        protected readonly long POLL_INTERVAL; // Number of 'ticks' per second
        protected readonly double POLL_MULTIPLIER; // Multiply by this to convert ticks to seconds

        public HighPerformanceTimer()
        {                       
            QueryPerformanceFrequency(out POLL_INTERVAL);
            POLL_MULTIPLIER = 1d / POLL_INTERVAL;
            QueryPerformanceCounter(out _monotonic);        
        }

        public long GetTicks()
        {
            long ticks;
            QueryPerformanceCounter(out ticks);
            return ticks;
        }
    }
}
