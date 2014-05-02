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
        private const int MAX_GLYPHS = 8;
        private const int MIN_GLYPHS = 1;

        private readonly float movementRate;
        private readonly float scale;
        private float marginScale = 1f; // 1 for normal; lower for glyphs closer together vertically               

        private readonly Rectangle _workingArea;

        private List<Glyph> _glyphs;

        // TODO: chance of glyph change; glyph index, color

        private Vector2f MaskPosition; // Stream position - scrolls down the screen
        private Vector2f GlyphPosition; // Individual glyphs location - doesn't change

        public GlyphStream(Rectangle workingArea)
        {
            _workingArea = workingArea;
            movementRate = GetRandom.Float(50, 300) * 0.1f;
            var numberOfGlyphs = GetRandom.Int(MIN_GLYPHS, MAX_GLYPHS);
            scale = GetRandom.Float(0.1f, 0.6f);

            GlyphPosition = new Vector2f(
                GetRandom.Int((int)-GlyphSize.X, (int) (_workingArea.Width + GlyphSize.X)),
                GetRandom.Int((int)-GlyphSize.Y, (int)(_workingArea.Height + GlyphSize.Y)));
            
            numberOfGlyphs = 5;
            GlyphPosition = new Vector2f(10,10);
            movementRate = 80;
            scale = GetRandom.Float(0.1f, 0.3f);

            _glyphs = new List<Glyph>();
            for (int i = 0; i < numberOfGlyphs; i++)
            {
                var y = GlyphPosition.Y + (i * Glyph.GLYPH_HEIGHT * scale * marginScale);

                if (y + Glyph.GLYPH_HEIGHT < 0) continue;
                if (y > workingArea.Height) continue;

                _glyphs.Add(new Glyph(new Vector2f(GlyphPosition.X, y), scale));
            }

            MaskPosition = new Vector2f(GlyphPosition.X, GlyphPosition.Y - MaskSize.Y);

        }


        public Vector2f MaskSize
        {
            get { return new Vector2f(GlyphSize.X, GlyphSize.Y * _glyphs.Count); } //TODO: handle margin scale
        }

        public Vector2f GlyphSize
        {
            get
            {
                return new Vector2f(Glyph.GLYPH_WIDTH * scale, Glyph.GLYPH_HEIGHT * scale);
            }
        }

        public bool IsExpired { get; private set; }

        public IntRect MaskArea()
        {
            return new IntRect(
                (int)MaskPosition.X,
                (int)MaskPosition.Y,
                (int)MaskPosition.X + (int)MaskSize.X,
                (int)MaskPosition.Y + (int)MaskSize.Y
                );
        }

        public void Render(RenderTarget canvas)
        {
            _glyphs.ForEach(g=>g.Render(canvas));

            if (Config.IsDebugRendering) // debug
            {
                Debug.DrawRect(canvas, new Color(0, 255, 0, 20),
                    MaskPosition.X, MaskPosition.Y,
                    MaskSize.X, MaskSize.Y,
                    //Glyph.GLYPH_WIDTH * 0.5f * scale,0);
                    0,0);

                Debug.DrawRect(canvas, new Color(0, 255, 255, 20),
                    MaskPosition.X, MaskPosition.Y,
                    200, 10,
                    0,5);

                Debug.DrawRect(canvas, new Color(0, 255, 255, 20),
                    MaskPosition.X, MaskPosition.Y + MaskSize.Y,
                    200, 10,
                    0,5);


            }
        }

        public void Update(ChronoEventArgs chronoArgs)
        {
            if (IsExpired) return;

            MaskPosition.Y += (float)(movementRate * chronoArgs.Delta);
            _glyphs.ForEach(g=>g.Update(chronoArgs, MaskArea()));

            CheckIfExpired();
        }

        private void CheckIfExpired()
        {
            if (MaskPosition.Y > _workingArea.Bottom ||
                MaskPosition.Y > GlyphPosition.Y + MaskSize.Y)
            {
                IsExpired = true;
            }
        }
    }
}