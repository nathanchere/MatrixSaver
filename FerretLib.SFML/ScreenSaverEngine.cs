using SFML.Window;

namespace FerretLib.SFML
{
    public class ScreenSaverEngine
    {
        private readonly ViewPortCollection _viewPorts;
        public IWorldEngine Engine { get; set; }    
        private bool _isFinished = false;

        /// <summary>
        /// .ctor
        /// </summary>
        public ScreenSaverEngine()
        {
            _viewPorts = new ViewPortCollection(true, true);
        }

        /// <summary>
        /// Tell instance to exit main loop after next iteration
        /// </summary>
        public void EndLoop()
        {
            _isFinished = true;
        }

        #region Common helpers
        public void BindEscapeToExit()
        {
            _viewPorts.KeyPressed += (o, e) => { if (e.Code == Keyboard.Key.Escape) _isFinished = true; };
        }
        #endregion

        /// <summary>
        /// Main loop
        /// </summary>
        public void Run()
        {
            Engine.Initialise(_viewPorts);
            while (!_isFinished)
            {
                FpsTimer.Update();
                _viewPorts.HandleEvents();
                Engine.Update();
                Engine.Render();         
            }
        }

        public double GetFps()
        {
            return FpsTimer.GetFps;
        }

    }
}
