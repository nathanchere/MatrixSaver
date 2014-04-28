using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace FerretLib.SFML
{
    //TODO: possibly change to use QueryPerformanceCounter
    internal class Chrono
    {
        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(out long lpFrequency);

        private long _monotonic;
        private FpsCounter _fps;
    
        private readonly long POLL_INTERVAL; // Number of 'ticks' per second
        private readonly double POLL_MULTIPLIER; // Multiply by this to convert ticks to seconds

        internal Chrono()
        {            
            QueryPerformanceFrequency(out POLL_INTERVAL);
            POLL_MULTIPLIER = 1d / POLL_INTERVAL;

            _fps = new FpsCounter(POLL_INTERVAL);
            QueryPerformanceCounter(out _monotonic);
        }

        internal ChronoEventArgs Update()
        {
            long ticks;
            QueryPerformanceCounter(out ticks);
            

            var delta = (ticks - _monotonic) * POLL_MULTIPLIER;
            var fps = _fps.Update(ticks);
           
            _monotonic = ticks;
            return new ChronoEventArgs(_monotonic, delta, fps);
        }

        internal class FpsCounter
        {
            private long _frames; // Number of frames elapsed since last FPS calculation
            private long _nextTicks; // When to next calculate FPS
            private float _fps; // Last calculated FPS

            private readonly long _timerFrequency;
            private readonly double _timerMultiplier;

            public FpsCounter(long timerFrequency)
            {
                _timerFrequency = timerFrequency;
                _timerMultiplier = 1d / _timerFrequency;
            }

            public float Update(long ticks)
            {
                _frames++;

                if (_nextTicks <= ticks)
                {
                    _fps = (float)Math.Round(_frames * (ticks - _nextTicks + _timerFrequency) * _timerMultiplier, 2);
                    _frames = 0;
                    _nextTicks = ticks + _timerFrequency;
                }

                return _fps;
            }
        }
    }

}
