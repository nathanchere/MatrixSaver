using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace FerretLib.SFML
{
    //TODO: possibly change to use QueryPerformanceCounter
    internal class Chrono
    {
        [DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int GetTickCount();

        private int _monotonic;
        private FpsCounter _fps;

        private const int POLL_INTERVAL = 1000;
        private const double POLL_MULTIPLIER = 1 / POLL_INTERVAL;        

        internal ChronoEventArgs Update()
        {
            var ticks = GetTickCount();

            var delta = (float)((ticks - _monotonic) * POLL_MULTIPLIER);

            var fps = _fps.Update(ticks);

            _monotonic = ticks;

            return new ChronoEventArgs(_monotonic, delta, fps);
        }

        internal class FpsCounter
        {
            private int _frames; // Number of frames elapsed since last FPS calculation
            private int _nextTicks; // When to next calculate FPS
            private float _fps; // Last calculated FPS

            public float Update(int ticks)
            {
                _frames++;

                if (_nextTicks <= ticks)
                {
                    _fps = (float)Math.Round(_frames * (ticks - _nextTicks + POLL_INTERVAL) * POLL_MULTIPLIER, 2);
                    _frames = 0;
                    _nextTicks = ticks + POLL_INTERVAL;
                }

                return _fps;
            }
        }
    }

}
