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

        private RenderTexture canvas;


        private GlyphManager glyphs;

        private ChronoDisplay chrono;

        public MatrixEngine()
        {

        }

        #region Contracts
        public void Render()
        {
            canvas.Clear(Color.Black);

            foreach (var viewport in _viewports) {
                // Render from global texture here
            }

            var cursorPosition = _viewports.CursorPosition();
            foreach (var viewport in _viewports) {
                viewport.Window.Clear(Color.Black);

                chrono.Render(viewport.Window);
                glyphs.Draw(viewport);

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
