using System;

namespace FerretLib.SFML
{
    internal class FpsCounter
    {
        private long _frames; // Number of frames elapsed since last FPS calculation
        private double _nextTicks; // When to next calculate FPS
        private float _fps; // Last calculated FPS       

        public float Update(double ticks)
        {
            _frames++;

            if (_nextTicks <= ticks)
            {
                _fps = (float)Math.Round(_frames * (ticks - _nextTicks + 1f), 2);
                _frames = 0;
                _nextTicks = ticks + 1f;
            }

            return _fps;
        }
    }
}