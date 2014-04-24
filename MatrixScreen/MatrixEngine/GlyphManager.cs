using System;
using System.Collections.Generic;
using System.IO;
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

        private const int MAX_STREAMS = 20;
        private const float CHANCE_OF_NEW_STREAM = 0.4f;

        private List<GlyphStream> streams; 

        public GlyphManager()
        {
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
            streams.ForEach(x => {                
                glyphSprite.TextureRect = new IntRect(GLYPH_WIDTH * (int)(DateTime.Now.Second * 0.25f), ((int)(DateTime.Now.Millisecond * 0.008) % 4) * GLYPH_HEIGHT, GLYPH_WIDTH, GLYPH_HEIGHT);
                glyphSprite.Position = viewport.GetLocalCoordinates(x.Position.ToVector2i());
                glyphSprite.Draw(viewport.Window, RenderStates.Default);
                glyphSprite.Color = new Color(0, 255, 0);
            });
        }

        public void Update()
        {
            streams.ForEach(x=>x.Update());

            // cull dead streams, etc
        }

        internal class GlyphStream
        {
            private float movementRate = 0.4f;

            public GlyphStream()
            {
                Position = new Vector2f(40,0);
            }

            public Vector2f Position;

            public void Update()
            {
                Position.Y += movementRate;
            }

            public void Draw()
            {
            }
        }
    }    
}