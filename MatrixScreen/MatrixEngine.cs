using System;
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

        const int GLYPH_TEXTURE_SIZE = 2048;
        const int GLYPH_WIDTH = GLYPH_TEXTURE_SIZE / 16;
        const int GLYPH_HEIGHT = GLYPH_TEXTURE_SIZE / 8;

        private Texture glyphTexture;
        private Sprite glyphSprite;

        public MatrixEngine()
        {
            text = new Text
            {
                Font = new Font(@"data\lekton.ttf"),
                CharacterSize = 12,
                Color = Color.Green,
            };

            glyphTexture = new Texture(@"data\glyphs.png") {Smooth = true, Repeated = false};
            glyphSprite = new Sprite(glyphTexture);
            glyphSprite.Origin = new Vector2f(GLYPH_WIDTH * 0.5f, GLYPH_HEIGHT * 0.5f);
        }

        #region IWorldEngine
        void IWorldEngine.Render()
        {
            int x = Mouse.GetPosition().X - _viewports.WorkingArea.Left;
            int y = Mouse.GetPosition().Y - _viewports.WorkingArea.Top;
            var globalCursorText = string.Format("Global cursor: {0}:{1}", x,y);

            foreach (var viewport in _viewports)
            {
                viewport.Window.Clear(Color.Black);

                text.Color = viewport.WorkingArea.Contains(Mouse.GetPosition().ToPoint()) ?
                    Color.Green : Color.Red;

                text.Position = new Vector2f(30, 30);
                text.DisplayedString = string.Format("Cursor: {0}:{1}", Mouse.GetPosition().X, Mouse.GetPosition().Y);
                viewport.Window.Draw(text);

                text.Position = new Vector2f(30, 50);
                text.DisplayedString = globalCursorText;
                viewport.Window.Draw(text);

                text.Position = new Vector2f(30, 70);
                text.DisplayedString = string.Format("Viewport #{0}; origin: {1},{2}",
                    viewport.ID,
                    viewport.WorkingArea.Left,
                    viewport.WorkingArea.Top);
                viewport.Window.Draw(text);

                text.Position = new Vector2f(30, 90);
                text.DisplayedString = string.Format("Global boundaries: t:{0},l:{1},r:{2},b{3}",
                    _viewports.WorkingArea.Top,
                    _viewports.WorkingArea.Left,
                    _viewports.WorkingArea.Right,
                    _viewports.WorkingArea.Bottom);
                viewport.Window.Draw(text);

                glyphSprite.TextureRect = new IntRect(GLYPH_WIDTH * (int)(DateTime.Now.Second * 0.25f), ((int)(DateTime.Now.Millisecond * 0.008)% 4) * GLYPH_HEIGHT, GLYPH_WIDTH, GLYPH_HEIGHT);
                glyphSprite.Position = Mouse.GetPosition().ToVector2f();
                glyphSprite.Draw(viewport.Window, RenderStates.Default);
                glyphSprite.Color = new Color(0,255,0);

                glyphSprite.Scale = new Vector2f(0.6f,0.6f);

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
