using System.Runtime.InteropServices;
using System.Threading;

namespace FerretLib.SFML
{
    internal class FpsLimiter : HighPerformanceTimer
    {
        private double _frequency;
        private double _nextTime;

        public void SetTargetFps(int targetFps)
        {
            _frequency = 1f / targetFps;
        }

        public FpsLimiter(int targetFps)
        {
            SetTargetFps(targetFps);
            _nextTime = GetTicks();
        }

        internal void Sleep()
        {
            while (GetTicks() < _nextTime) Thread.Sleep(1);
            _nextTime += _frequency;
        }
    }
}
