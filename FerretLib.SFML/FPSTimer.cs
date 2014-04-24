using System;
using System.Runtime.InteropServices;

namespace FerretLib.SFML
{
    internal static class FpsTimer
    {
        [DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int GetTickCount();

        private static int _frames;
        private static int _nextTicks;
        private static double _fps;

        private const int POLL_INTERVAL = 1000;
        private const double POLL_MULTIPLIER = 1/POLL_INTERVAL;

        internal static void Update()
        {
            _frames += 1;
            var currentTicks = GetTickCount();
            if (_nextTicks > currentTicks) return;

            _fps = Math.Round(_frames * (currentTicks - _nextTicks + POLL_INTERVAL) * POLL_MULTIPLIER, 2);

            _frames = 0;
            _nextTicks = currentTicks + POLL_INTERVAL;            
        }

        internal static double GetFps
        {
            get { return _fps; }
        }
    }

}
