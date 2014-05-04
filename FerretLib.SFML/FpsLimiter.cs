using System.Runtime.InteropServices;

namespace FerretLib.SFML
{
    internal class FpsLimiter
    {

        //public int TargetFps { get; private set; }

        //private long _monotonic;
        //private FpsCounter _fps;
    
        //private readonly long POLL_INTERVAL; // Number of 'ticks' per second
        //private readonly double POLL_MULTIPLIER; // Multiply by this to convert ticks to seconds

        //internal FpsLimiter()
        //{            
        //    QueryPerformanceFrequency(out POLL_INTERVAL);
        //    POLL_MULTIPLIER = 1d / POLL_INTERVAL;

        //    _fps = new FpsCounter(POLL_INTERVAL);
        //    QueryPerformanceCounter(out _monotonic);
        //}

        //internal ChronoEventArgs Update()
        //{
        //    long ticks;
        //    QueryPerformanceCounter(out ticks);
            

        //    var delta = (ticks - _monotonic) * POLL_MULTIPLIER;
        //    var fps = _fps.Update(ticks);
           
        //    _monotonic = ticks;
        //    return new ChronoEventArgs(_monotonic, delta, fps);
        //}

        //public FpsLimiter(int targetFps)
        //{
        //    TargetFps = targetFps;
        //}
    }
}
