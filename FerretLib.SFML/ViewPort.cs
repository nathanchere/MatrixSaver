using System;
using System.Drawing;
using System.Windows.Forms;
using SFML.Graphics;
using SFML.Window;

namespace FerretLib.SFML
{
    public class ViewPort : IDisposable
    {
        public RenderWindow Window { get; protected set; }

        /// <summary>
        /// Keeps track of the viewport's position relative to the total desktop/display area available
        /// </summary>
        public System.Drawing.Rectangle WorkingArea { get; protected set; }

        public ViewPort(Screen screen, bool isFullScreen)
        {            
            Styles style = Styles.None;

            if (isFullScreen)
            {
                style = Styles.Fullscreen;
                WorkingArea = screen.WorkingArea;
            } else {                
                style = Styles.Resize;
                WorkingArea = new Rectangle(
                        screen.WorkingArea.Left,
                        screen.WorkingArea.Top,
                        (int)(screen.WorkingArea.Width * 0.5),
                        (int)(screen.WorkingArea.Height * 0.5)
                    );
            }

            Window = new RenderWindow(
                new VideoMode(
                    (uint)WorkingArea.Width,
                    (uint)WorkingArea.Height
                ),
                "Screensaver [debug]",
                style,
                new ContextSettings(32, 0, 0)
                );
            
            Window.Position = new Vector2i(WorkingArea.Left, WorkingArea.Top);
            Window.SetTitle(Window.Position.X + "," + Window.Position.Y);
        }

        #region IDisposable Support
        private bool _isDisposed = false;

        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if(_isDisposed) return;
            if(Window != null) Window.Close();
            Window = null;

            // Any other unmanaged graphics stuff - release here

            _isDisposed = true;
        }
        #endregion

    }
}
