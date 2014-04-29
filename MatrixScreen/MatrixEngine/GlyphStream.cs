using System;
using System.Collections.Generic;
using System.Drawing;
using FerretLib.SFML;
using SFML.Graphics;
using SFML.Window;
using Color = SFML.Graphics.Color;

namespace MatrixScreen
{
    public class GlyphStream : IEntity
    {
        private readonly float movementRate;
        private readonly float scale;
        private float marginScale = 0.8f; // 1 for normal; lower for glyphs closer together vertically               

        private readonly Rectangle _workingArea;

        private List<Glyph> _glyphs;

        // TODO: chance of glyph change; glyph index, color

        private Vector2f Position; // Stream position - scrolls down the screen
        private Vector2f GlyphPosition; // Individual glyphs location - doesn't change

        public GlyphStream(Rectangle workingArea)
        {
            _workingArea = workingArea;
            movementRate = GetRandom.Float(50, 300);
            var numberOfGlyphs = GetRandom.Int(3, 12);
            scale = GetRandom.Float(0.1f, 0.6f);

            GlyphPosition = new Vector2f(
                GetRandom.Int((int)-GlyphSize.X, (int) (_workingArea.Width + GlyphSize.X)),
                GetRandom.Int((int)-GlyphSize.Y, (int)(_workingArea.Height + GlyphSize.Y)));

            Position = new Vector2f(GlyphPosition.X, GlyphPosition.Y - GlyphSize.Y);

            _glyphs = new List<Glyph>();
            for (int i = 0; i < numberOfGlyphs; i++)
            {
                var y = GlyphPosition.Y + (i * Glyph.GLYPH_HEIGHT * scale * marginScale);

                if (y + Glyph.GLYPH_HEIGHT < 0) continue;
                if (y > workingArea.Height) continue;

                _glyphs.Add(new Glyph(new Vector2f(GlyphPosition.X, y), scale));
            }
        }


        public Vector2f Size
        {
            get { return new Vector2f(GlyphSize.X, GlyphSize.Y + (GlyphSize.Y * (_glyphs.Count - 1) * marginScale)); }
        }

        public Vector2f GlyphSize
        {
            get
            {
                return new Vector2f(Glyph.GLYPH_WIDTH * scale, Glyph.GLYPH_HEIGHT * scale);
            }
        }

        public bool IsExpired { get; private set; }

        public IntRect DrawingArea()
        {
            return new IntRect(
                (int)Position.X,
                (int)Position.Y,
                (int)Position.X + (int)Size.X,
                (int)Position.Y + (int)Size.Y
                );
        }

        public void Render(RenderTarget canvas)
        {
            _glyphs.ForEach(g=>g.Render(canvas));

            if (Config.IsDebugRendering) // debug
            {
                var shape = new RectangleShape(Size)
                {
                    FillColor = new Color(0, 255, 0, 40),
                    Position = Position,
                    Origin = new Vector2f(Glyph.GLYPH_WIDTH * 0.5f * scale, 0),
                };
                shape.Draw(canvas, RenderStates.Default);
            }
        }

        public void Update(ChronoEventArgs chronoArgs)
        {
            if (IsExpired) return;

            Position.Y += (float)(movementRate * chronoArgs.Delta);

            CheckIfExpired();
        }

        private void CheckIfExpired()
        {
            if (Position.Y > _workingArea.Bottom ||
                Position.Y > GlyphPosition.Y + Size.Y)
            {
                IsExpired = true;
            }
        }
    }
}