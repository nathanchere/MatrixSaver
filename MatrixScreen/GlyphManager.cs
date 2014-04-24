using System;
using FerretLib.SFML;
using SFML.Graphics;
using SFML.Window;

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
}