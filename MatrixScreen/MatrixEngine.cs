using System;
using FerretLib.SFML;
using SFML.Graphics;
using SFML.Window;
using Color = SFML.Graphics.Color;
using Font = SFML.Graphics.Font;

namespace MatrixScreen
{
    public class GlyphManager
    {
        const int GLYPH_TEXTURE_SIZE = 2048;
        const int GLYPH_WIDTH = GLYPH_TEXTURE_SIZE / 16;
        const int GLYPH_HEIGHT = GLYPH_TEXTURE_SIZE / 8;

        private Texture glyphTexture;
        private Sprite glyphSprite;

        public GlyphManager()
        {
            glyphTexture = new Texture(@"data\glyphs.png") { Smooth = true, Repeated = false };
            glyphSprite = new Sprite(glyphTexture);
            glyphSprite.Origin = new Vector2f(GLYPH_WIDTH * 0.5f, GLYPH_HEIGHT * 0.5f);
        }

        public void Draw(ViewPort viewport)
        {
            glyphSprite.TextureRect = new IntRect(GLYPH_WIDTH * (int)(DateTime.Now.Second * 0.25f), ((int)(DateTime.Now.Millisecond * 0.008) % 4) * GLYPH_HEIGHT, GLYPH_WIDTH, GLYPH_HEIGHT);
            glyphSprite.Position = viewport.GetLocalCoordinates(Mouse.GetPosition());
            glyphSprite.Draw(viewport.Window, RenderStates.Default);
            glyphSprite.Color = new Color(0, 255, 0);

            glyphSprite.Scale = new Vector2f(0.6f, 0.6f);
        }
    }

    public class MatrixEngine : IWorldEngine
    {
        private ViewPortCollection _viewports;
        private Text text;
        private GlyphManager glyphs;

        public MatrixEngine()
        {
            text = new Text
            {
                Font = new Font(@"data\lekton.ttf"),
                CharacterSize = 14,
                Color = Color.Green,
            };

            glyphs = new GlyphManager();
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

        void IWorldEngine.Update()
        {

        }

        void IWorldEngine.Initialise(ViewPortCollection viewports)
        {
            _viewports = viewports;
        }
        #endregion

    }
}
