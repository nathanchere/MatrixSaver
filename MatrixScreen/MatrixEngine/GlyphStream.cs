using System;
using FerretLib.SFML;
using SFML.Graphics;
using SFML.Window;

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
            get
            {
                return new Vector2f(GLYPH_WIDTH * scale, GLYPH_HEIGHT * numberOfGlyphs * scale);
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
            glyphSprite.Color = new Color(0, 255, 0, 190);
            glyphSprite.Scale = new Vector2f(0.6f, 0.6f);
            glyphSprite.TextureRect = new IntRect(GLYPH_WIDTH * (int)(DateTime.Now.Second * 0.25f), ((int)(DateTime.Now.Millisecond * 0.008) % 4) * GLYPH_HEIGHT, GLYPH_WIDTH, GLYPH_HEIGHT);
            glyphSprite.Position = Mouse.GetPosition().ToVector2f();
            glyphSprite.Draw(canvas, RenderStates.Default);        

            var shape = new RectangleShape(Size) {
                FillColor = new Color(0,255,0,40),
                Position = Position,
                Origin = new Vector2f(GLYPH_WIDTH * 0.5f, 0),
            };
            shape.Draw(canvas, RenderStates.Default);

            glyphSprite.TextureRect = new IntRect(GLYPH_WIDTH * (int)(DateTime.Now.Second * 0.25f), ((int)(DateTime.Now.Millisecond * 0.008) % 4) * GLYPH_HEIGHT, GLYPH_WIDTH, GLYPH_HEIGHT);
            glyphSprite.Position = GlyphPosition;
            glyphSprite.Draw(canvas, RenderStates.Default);
        }

        public void Update(ChronoEventArgs chronoArgs)
        {
            throw new NotImplementedException();
        }
    }
}