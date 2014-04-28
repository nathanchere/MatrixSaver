using SFML.Graphics;
using Sfml = SFML;
using SFML.Window;

namespace FerretLib.SFML
{
    public class ScreenSaverEngine
    {
        public IWorldEngine Engine { get; set; }

        private readonly ViewPortCollection _viewPorts;        
        private RenderTexture _canvas;

        private readonly Chrono _chrono;
        private bool _isFinished = false;

        /// <summary>
        /// .ctor
        /// </summary>
        public ScreenSaverEngine()
        {
            _viewPorts = new ViewPortCollection(false, false);
            _canvas = new RenderTexture((uint) _viewPorts.WorkingArea.Width, (uint) _viewPorts.WorkingArea.Height, false);
            _canvas.Clear(Color.Black);
            _canvas.Display(); // Needed due to FBO causing inverted co-ords otherwise
            _chrono = new Chrono();
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
                var updateArgs = _chrono.Update();

                _viewPorts.HandleEvents();
                Engine.Update(updateArgs);
                Engine.Render(_canvas);

                _viewPorts.Draw(_canvas);
            }
        }
        
    }
}
