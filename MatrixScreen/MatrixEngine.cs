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
        private Text text;
        private GlyphManager glyphs;

        private readonly Rectangle workingArea;

        public MatrixEngine()
        {
            text = new Text
            {
                Font = new Font(@"data\lekton.ttf"),
                CharacterSize = 14,
                Color = Color.Green,
            };

            glyphs = new GlyphManager();
            //workingArea = _viewports.WorkingArea.
        }

        #region IWorldEngine
        void IWorldEngine.Render()
        {
            var cursorPosition = _viewports.CursorPosition();
            foreach (var viewport in _viewports)
            {
                viewport.Window.Clear(Color.Black);

                text.Color = viewport.WorkingArea.Contains(Mouse.GetPosition().ToPoint()) ?
                    Color.Green : Color.Red;

                text.Position = new Vector2f(30, 30);
                text.DisplayedString = string.Format("Cursor: {0}:{1}", Mouse.GetPosition().X, Mouse.GetPosition().Y);
                viewport.Window.Draw(text);
                glyphs.Draw(viewport);

                viewport.Window.Display();
            }
        }

        void IWorldEngine.Update(ChronoEventArgs chronoArgs)
        {
            glyphs.Update(chronoArgs.Delta, _viewports.WorkingArea);
        }

        void IWorldEngine.Initialise(ViewPortCollection viewports)
        {
            _viewports = viewports;
        }
        #endregion

    }
}
