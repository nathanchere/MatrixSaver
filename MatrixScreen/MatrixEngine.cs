using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using FerretLib.SFML;
using SFML.Graphics;
using SFML.Window;
using Color = SFML.Graphics.Color;
using Font = SFML.Graphics.Font;

namespace MatrixScreen
{
    public class MatrixEngine : IWorldEngine
    {
        private List<GlyphStreamManager> _glyphsStreams;

        private ChronoDisplay _chrono;
        private MatrixConfig _settings;

        public MatrixEngine(MatrixConfig settings)
        {
            _chrono = new ChronoDisplay();
            _settings = settings;
        }

        public void Render(RenderTarget canvas)
        {
            ((RenderTexture)canvas).Display();
            ((RenderTexture)canvas).Clear(Color.Black);
            _glyphsStreams.ForEach(x=>x.Render(canvas));

            _chrono.Render(canvas);
        }

        public void Update(ChronoEventArgs chronoArgs)
        {
            _chrono.Update(chronoArgs);
            _glyphsStreams.ForEach(x=>x.Update(chronoArgs));
        }

        void IWorldEngine.Initialise(ViewPortCollection viewports)
        {
            var area = new Vector2u(
                (uint)viewports.WorkingArea.Width,
                (uint)viewports.WorkingArea.Height);

            _glyphsStreams = new List<GlyphStreamManager>();
            foreach (var setting in _settings.RenderLayers)
            {
                _glyphsStreams.Add(new GlyphStreamManager(setting, area));
            }            
        }
    }
}
