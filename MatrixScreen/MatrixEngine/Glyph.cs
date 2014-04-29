using System;
using FerretLib.SFML;
using SFML.Graphics;
using SFML.Window;

namespace MatrixScreen
{
    public class Glyph : IEntity
    {
        public const int MAX_INDEX = 92; // maximum glyph character index

        public const int GLYPH_TEXTURE_COLUMNS = 16;
        public const int GLYPH_TEXTURE_ROWS = 8;

        public const int GLYPH_TEXTURE_SIZE = 2048;
        public const int GLYPH_WIDTH = GLYPH_TEXTURE_SIZE / GLYPH_TEXTURE_COLUMNS;
        public const int GLYPH_HEIGHT = GLYPH_TEXTURE_SIZE / GLYPH_TEXTURE_ROWS;

        private Sprite sprite;

        private static Texture texture = new Texture(@"data\glyphs.png")
        {
            Smooth = true,
            Repeated = false,
        };

        public Glyph(Vector2f location)
        {
            sprite = new Sprite(texture);
            sprite.Origin = new Vector2f(GLYPH_WIDTH * 0.5f, GLYPH_HEIGHT * 0.5f);

            // set sprite position
        }

        public void Render(RenderTarget target)
        {            
                //var y = GlyphPosition.Y + (GLYPH_HEIGHT * scale * marginScale) * i;
                //if (
                //    DrawingArea().Top + DrawingArea().Height > y
                //    && DrawingArea().Top < y + (GLYPH_HEIGHT * scale * marginScale)
                //    )
                //{
                //    glyphSprite.Color = new Color(0, 255, 0, 190);
                //    glyphSprite.Position = new Vector2f(GlyphPosition.X, y);
                //    glyphSprite.Draw(canvas, RenderStates.Default);
                //}
                //else
                //{
                //    glyphSprite.Color = new Color(255, 255, 255, 5);
                //}            
        }

        public void Update(ChronoEventArgs chronoArgs)
        {
            sprite.TextureRect = new IntRect(GLYPH_WIDTH * (int)(DateTime.Now.Second * 0.25f), ((int)(DateTime.Now.Millisecond * 0.008) % 4) * GLYPH_HEIGHT, GLYPH_WIDTH, GLYPH_HEIGHT);
        }
    }
}