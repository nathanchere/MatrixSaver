using System.Drawing;
using FerretLib.SFML;
using SFML.Graphics;
using SFML.Window;
using Color = SFML.Graphics.Color;
using Font = SFML.Graphics.Font;

namespace MatrixScreen
{
    public class MatrixEngine : IWorldEngine
    {
        private ViewPortCollection _viewports;

        private GlyphManager glyphs;

        private ChronoDisplay chrono;

        public MatrixEngine()
        {
            chrono = new ChronoDisplay();
        }

        #region Contracts
        public void Render(RenderTarget canvas)
        {
            canvas.Clear(Color.Black);            

            var cursorPosition = _viewports.CursorPosition();
            foreach (var viewport in _viewports) {
                viewport.Window.Clear(Color.Black);

                chrono.Render(canvas);
                //glyphs.Draw(canvas);

                viewport.Window.Display();
            }
        }

        public void Update(ChronoEventArgs chronoArgs)
        {
            chrono.Update(chronoArgs);

            glyphs.Update(chronoArgs.Delta);
        }

        void IWorldEngine.Initialise(ViewPortCollection viewports)
        {
            _viewports = viewports;
            canvas = new RenderTexture((uint)viewports.WorkingArea.Width, (uint)viewports.WorkingArea.Height, false);

            glyphs = new GlyphManager(canvas.Size);
        }
        #endregion

    }
}
