using System;
using System.Runtime.InteropServices;

namespace FerretLib.SFML.Utility
{
    internal static class FPSTimer
    {
        [DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int GetTickCount();

        private static int _frames;
        private static int _nextTicks;
        private static double _FPS;

        private const int POLL_INTERVAL = 1000;
        private static readonly double POLL_MULTIPLIER = 1 / POLL_INTERVAL;

        internal static void Update()
        {
            _frames += 1;
            int CurrentTicks = GetTickCount();
            if (_nextTicks > CurrentTicks)
                return;

            _FPS = Math.Round(_frames * (CurrentTicks - _nextTicks + POLL_INTERVAL) * POLL_MULTIPLIER, 2);

            _frames = 0;
            _nextTicks = CurrentTicks + POLL_INTERVAL;            
        }

        internal static double GetFPS
        {
            get { return _FPS; }
        }
    }

}
