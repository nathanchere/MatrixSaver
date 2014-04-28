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
        private GlyphStreamManager _glyphsStream;

        private ChronoDisplay chrono;

        public MatrixEngine()
        {
            chrono = new ChronoDisplay();
        }

        #region Contracts       
        public void Render(RenderTarget canvas)
        {
            ((RenderTexture)canvas).Display();
            ((RenderTexture)canvas).Clear(Color.Black);
            _glyphsStream.Render(canvas);
            chrono.Render(canvas);
        }

        public void Update(ChronoEventArgs chronoArgs)
        {
            chrono.Update(chronoArgs);
            _glyphsStream.Update(chronoArgs);
        }

        void IWorldEngine.Initialise(ViewPortCollection viewports)
        {
            var area = new Vector2u(
                (uint)viewports.WorkingArea.Width,
                (uint)viewports.WorkingArea.Height);
            _glyphsStream = new GlyphStreamManager(area);
        }
        #endregion

    }
}
