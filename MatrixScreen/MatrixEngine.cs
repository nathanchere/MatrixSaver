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
            glyphs.Render(canvas);
            chrono.Render(canvas);
        }

        public void Update(ChronoEventArgs chronoArgs)
        {
            chrono.Update(chronoArgs);
            glyphs.Update(chronoArgs);
        }

        void IWorldEngine.Initialise(ViewPortCollection viewports)
        {
            var area = new Vector2u(
                (uint)viewports.WorkingArea.Width,
                (uint)viewports.WorkingArea.Height);
            glyphs = new GlyphManager(area);
        }
        #endregion

    }
}
