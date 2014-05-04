using System;

namespace FerretLib.SFML
{
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