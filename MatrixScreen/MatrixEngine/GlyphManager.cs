using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using FerretLib.SFML;
using SFML.Graphics;
using SFML.Window;
using Color = SFML.Graphics.Color;

namespace MatrixScreen
{
    public class GlyphManager : IEntity
    {
        const int GLYPH_TEXTURE_SIZE = 2048;
        const int GLYPH_WIDTH = GLYPH_TEXTURE_SIZE / 16;
        const int GLYPH_HEIGHT = GLYPH_TEXTURE_SIZE / 8;

        private Texture glyphTexture;
        private Sprite glyphSprite;

        private const int MAX_STREAMS = 20;
        private const float CHANCE_OF_NEW_STREAM = 0.4f;

        private readonly Rectangle _workingArea;

        private List<GlyphStream> streams;

        public GlyphManager(Vector2u workingArea)
        {
            _workingArea = workingArea.ToRectangle();

            glyphTexture = new Texture(@"data\glyphs.png") {
                Smooth = true, Repeated = false
            };
            glyphSprite = new Sprite(glyphTexture);
            glyphSprite.Origin = new Vector2f(GLYPH_WIDTH * 0.5f, GLYPH_HEIGHT * 0.5f);

            streams = new List<GlyphStream>();
        }

        private void AddNewGlyphStream()
        {
            var stream = new GlyphStream();
            stream.movementRate = GetRandom.Float(50, 300);
            stream.numberOfGlyphs = GetRandom.Int(3,6);            
            stream.scale = GetRandom.Float(0.1f, 1.0f);
                        
            stream.Position = new Vector2f(GetRandom.Int(0, _workingArea.Width), 0 - stream.Size.Y);
            stream.GlyphPosition = new Vector2f(stream.Position.X, GetRandom.Float(0, 1080));


            streams.Add(stream);
        }

        public void Render(RenderTarget canvas)
        {
            glyphSprite.Color = new Color(0, 255, 0, 190);
            glyphSprite.Scale = new Vector2f(0.6f, 0.6f);
            glyphSprite.TextureRect = new IntRect(GLYPH_WIDTH * (int)(DateTime.Now.Second * 0.25f), ((int)(DateTime.Now.Millisecond * 0.008) % 4) * GLYPH_HEIGHT, GLYPH_WIDTH, GLYPH_HEIGHT);
            glyphSprite.Position = Mouse.GetPosition().ToVector2f();
            glyphSprite.Draw(canvas, RenderStates.Default);            

            glyphSprite.Scale = new Vector2f(0.4f, 0.4f);
            streams.ForEach(x => {                
                var shape = new RectangleShape(x.Size) {
                    FillColor = new Color(0,255,0,40),
                    Position = x.Position,
                    Origin = new Vector2f(GLYPH_WIDTH * 0.5f, 0),
                };
                shape.Draw(canvas, RenderStates.Default);

                glyphSprite.TextureRect = new IntRect(GLYPH_WIDTH * (int)(DateTime.Now.Second * 0.25f), ((int)(DateTime.Now.Millisecond * 0.008) % 4) * GLYPH_HEIGHT, GLYPH_WIDTH, GLYPH_HEIGHT);
                glyphSprite.Position = x.GlyphPosition;
                glyphSprite.Draw(canvas, RenderStates.Default);
            });
        }

        public void Update(ChronoEventArgs chronoArgs)
        {
            streams.ForEach(x => x.Update(chronoArgs.Delta));

            streams = streams.Where(x => !(x.Position.Y > _workingArea.Bottom)).ToList();

            while (streams.Count < MAX_STREAMS) AddNewGlyphStream();
        }


        internal class GlyphStream
        {
            public float movementRate = 120f;
            public int numberOfGlyphs = 6;
            public float scale = 1.0f;
            
            // TODO: list of glyphs
            // TODO: chance of glyph change; glyph index, color

            public Vector2f Position; // Stream position - scrolls down the screen
            public Vector2f GlyphPosition; // Individual glyphs location - doesn't change

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
        }
    }
}