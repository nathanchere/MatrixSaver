using System.Runtime.InteropServices;
using System.Threading;

namespace FerretLib.SFML
{
    internal class Chrono : HighPerformanceTimer
    {                
        private FpsCounter _fps;
        protected double _monotonic;
    
        internal Chrono() : base()
        {
            long ticks;
            QueryPerformanceCounter(out ticks);
            _monotonic = ticks * POLL_MULTIPLIER;

            _fps = new FpsCounter();
        }

        internal ChronoEventArgs Update()
        {
            var ticks = GetTicks();
            
            var delta = ticks - _monotonic;
            var fps = _fps.Update(ticks);
           
            _monotonic = ticks;
            return new ChronoEventArgs(_monotonic, delta, fps);
        }
    }
}
