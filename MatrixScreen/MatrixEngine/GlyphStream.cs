using System;
using System.Drawing;
using FerretLib.SFML;
using SFML.Graphics;
using SFML.Window;
using Color = SFML.Graphics.Color;

namespace MatrixScreen
{
    internal class GlyphStream : IEntity
    {
        const int GLYPH_TEXTURE_SIZE = 2048;
        const int GLYPH_WIDTH = GLYPH_TEXTURE_SIZE / 16;
        const int GLYPH_HEIGHT = GLYPH_TEXTURE_SIZE / 8;

        public float movementRate = 120f;
        public int numberOfGlyphs = 6;
        public float scale = 1.0f;
        public float marginScale = 0.8f; // 1 for normal; lower for glyphs closer together vertically

        private static Texture glyphTexture = new Texture(@"data\glyphs.png")
        {
            Smooth = true,
            Repeated = false
        };

        private static Sprite glyphSprite;

        // TODO: list of glyphs
        // TODO: chance of glyph change; glyph index, color

        public Vector2f Position; // Stream position - scrolls down the screen
        public Vector2f GlyphPosition; // Individual glyphs location - doesn't change

        public GlyphStream()
        {
            glyphSprite = new Sprite(glyphTexture);
            glyphSprite.Origin = new Vector2f(GLYPH_WIDTH * 0.5f, GLYPH_HEIGHT * 0.5f);
        }


        public Vector2f Size
        {
            get { return new Vector2f(GlyphSize.X, GlyphSize.Y + (GlyphSize.Y * (numberOfGlyphs - 1) * marginScale)); }
        }

        public Vector2f GlyphSize
        {
            get
            {
                return new Vector2f(GLYPH_WIDTH * scale, GLYPH_HEIGHT * scale);
            }
        }

        public IntRect DrawingArea()
        {
            return new IntRect(
                (int)Position.X,
                (int)Position.Y,
                (int)Position.X + (int)Size.X,
                (int)Position.Y + (int)Size.Y
                );
        }

        public void Update(double delta)
        {
            Position.Y += (float)(movementRate * delta);
        }

        public void Draw()
        {
        }

        public void Render(RenderTarget canvas)
        {
            glyphSprite.Scale = new Vector2f(scale, scale);
            //glyphSprite.Origin = new Vector2f(GLYPH_WIDTH * 0.5f * scale, 0);
            glyphSprite.TextureRect = new IntRect(GLYPH_WIDTH * (int)(DateTime.Now.Second * 0.25f), ((int)(DateTime.Now.Millisecond * 0.008) % 4) * GLYPH_HEIGHT, GLYPH_WIDTH, GLYPH_HEIGHT);
            for (int i = 0; i < numberOfGlyphs; i++)
            {
                var y = GlyphPosition.Y + (GLYPH_HEIGHT * scale * marginScale) * i;
                if (
                    DrawingArea().Top + DrawingArea().Height > y
                    && DrawingArea().Top < y + (GLYPH_HEIGHT * scale * marginScale)
                    )
                {
                    glyphSprite.Color = new Color(0, 255, 0, 190);
                    glyphSprite.Position = new Vector2f(GlyphPosition.X, y);
                    glyphSprite.Draw(canvas, RenderStates.Default);
                }
                else
                {
                    glyphSprite.Color = new Color(255, 255, 255, 5);
                }
            }

            //var shape = new RectangleShape(Size)
            //{
            //    FillColor = new Color(0, 255, 0, 40),
            //    Position = Position,
            //    Origin = new Vector2f(GLYPH_WIDTH * 0.5f * scale, 0),
            //};
            //shape.Draw(canvas, RenderStates.Default);

            //glyphSprite.Draw(canvas, RenderStates.Default);
        }

        public void Update(ChronoEventArgs chronoArgs)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Hide any extra glyphs in glyph strings that extend beyond screen boundaries
        /// </summary>
        public void ClipGlyphs(Rectangle workingArea)
        {
            while (GlyphPosition.Y < (0 - GlyphSize.Y))
            {
                GlyphPosition.Y += GLYPH_HEIGHT;
                numberOfGlyphs--;
            }

            while (GlyphPosition.Y + Size.Y > workingArea.Height + GLYPH_HEIGHT)
            {
                numberOfGlyphs--;
            }
        }
    }
}