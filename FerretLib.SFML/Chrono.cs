using System.Runtime.InteropServices;
using System.Threading;

namespace FerretLib.SFML
{
    internal class Chrono : HighPerformanceTimer
    {                
        private FpsCounter _fps;
    
        internal Chrono() : base()
        {            
            _fps = new FpsCounter(POLL_INTERVAL);
        }

        internal ChronoEventArgs Update()
        {
            var ticks = GetTicks();
            
            var delta = (ticks - _monotonic) * POLL_MULTIPLIER;
            var fps = _fps.Update(ticks);
           
            _monotonic = ticks;
            return new ChronoEventArgs(_monotonic, delta, fps);
        }
    }
}
