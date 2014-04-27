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
    public class GlyphManager
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
            glyphTexture = new Texture(@"data\glyphs.png") { Smooth = true, Repeated = false };
            glyphSprite = new Sprite(glyphTexture);
            glyphSprite.Origin = new Vector2f(GLYPH_WIDTH * 0.5f, GLYPH_HEIGHT * 0.5f);
            streams = new List<GlyphStream>();
            streams.Add(new GlyphStream());
        }

        public void Draw(ViewPort viewport)
        {
            glyphSprite.Scale = new Vector2f(0.6f, 0.6f);
            glyphSprite.TextureRect = new IntRect(GLYPH_WIDTH * (int)(DateTime.Now.Second * 0.25f), ((int)(DateTime.Now.Millisecond * 0.008) % 4) * GLYPH_HEIGHT, GLYPH_WIDTH, GLYPH_HEIGHT);
            glyphSprite.Position = viewport.GetLocalCoordinates(Mouse.GetPosition());
            glyphSprite.Draw(viewport.Window, RenderStates.Default);
            glyphSprite.Color = new Color(0, 255, 0);

            glyphSprite.Scale = new Vector2f(0.4f, 0.4f);
            streams.ForEach(x =>
            {
                var shape = new RectangleShape(x.DrawingArea().Size.ToVector2f())
                {
                    FillColor = new Color(60,255,0,30),
                    Position = viewport.GetLocalCoordinates(x.DrawingArea().Location.ToVector2i()),
                    Origin = new Vector2f(GLYPH_WIDTH * 0.5f, 0),
                };
                shape.Draw(viewport.Window, RenderStates.Default);

                glyphSprite.TextureRect = new IntRect(GLYPH_WIDTH * (int)(DateTime.Now.Second * 0.25f), ((int)(DateTime.Now.Millisecond * 0.008) % 4) * GLYPH_HEIGHT, GLYPH_WIDTH, GLYPH_HEIGHT);
                glyphSprite.Position = viewport.GetLocalCoordinates(x.GlyphPosition.ToVector2i());
                glyphSprite.Draw(viewport.Window, RenderStates.Default);
                glyphSprite.Color = new Color(0, 255, 0);
            });
        }

        public void Update(double delta)
        {
            streams.ForEach(x=>x.Update(delta));

            streams = streams.Where(x => !(x.Position.Y > _workingArea.Bottom)).ToList();
            
            while (streams.Count < MAX_STREAMS) streams.Add(new GlyphStream());            
        }

        internal class GlyphStream
        {
            private float movementRate = 120f;
            private int numberOfGlyphs = 6;

            public GlyphStream()
            {                
                movementRate = GetRandom.Float(50,300);
                GlyphPosition = new Vector2f(40,220);
                Position = new Vector2f(GetRandom.Int(0, 1920), -Size.Y);
            }

            public Vector2f Position; // Stream position - scrolls down the screen
            public Vector2f GlyphPosition; // Individual glyphs location - doesn't change

            public Vector2f Size
            {
                get { return new Vector2f(GLYPH_WIDTH, GLYPH_HEIGHT * numberOfGlyphs); }
            }

            public Rectangle DrawingArea()
            {
                return new Rectangle(
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