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

        private Text text;
        private GlyphManager glyphs;

        private ChronoEventArgs _chrono;

        public MatrixEngine()
        {
            text = new Text
            {
                Font = new Font(@"data\lekton.ttf"),
                CharacterSize = 14,
                Color = Color.Green,
            };
        }

        #region IWorldEngine
        void IWorldEngine.Render()
        {
            canvas.Clear(Color.Black);

            foreach (var viewport in _viewports)
            {
                
            }

            var cursorPosition = _viewports.CursorPosition();
            foreach (var viewport in _viewports)
            {
                viewport.Window.Clear(Color.Black);

                text.Color = viewport.WorkingArea.Contains(Mouse.GetPosition().ToPoint()) ?
                    Color.Green : Color.Red;


                text.Position = new Vector2f(30, 30);
                text.DisplayedString = string.Format("Cursor: {0}:{1}\n{2:0.00000}d // {3:#####0.00}FPS",
                    cursorPosition.X, cursorPosition.Y,
                    _chrono.Delta,
                    _chrono.Fps
                    );
                viewport.Window.Draw(text);
                glyphs.Draw(viewport);

                viewport.Window.Display();
            }
        }

        void IWorldEngine.Update(ChronoEventArgs chronoArgs)
        {
            glyphs.Update(chronoArgs.Delta);
            _chrono = chronoArgs;
        }

        void IWorldEngine.Initialise(ViewPortCollection viewports)
        {
            _viewports = viewports;            
            canvas = new RenderTexture((uint) viewports.WorkingArea.Width, (uint) viewports.WorkingArea.Height, false);

            glyphs = new GlyphManager(canvas.Size);
        }
        #endregion

    }
}
