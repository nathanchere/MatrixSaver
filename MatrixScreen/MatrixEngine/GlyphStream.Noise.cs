using System.Collections.Generic;
using System.Drawing;
using FerretLib.SFML;
using SFML.Graphics;
using SFML.Window;

namespace MatrixScreen
{
    /// <summary>
    /// Not what I intended but looks cool enough to keep to maybe work in later
    /// </summary>
    public class GlyphStreamNoise : IEntity
    {
        private readonly float _movementRate;
        private readonly float _scale;

        private readonly Rectangle _workingArea;
        private readonly List<Glyph> _glyphs;
        private readonly Vector2f _maskSize;

        private Vector2f MaskPosition; // Stream position - scrolls down the screen
        private Vector2f GlyphPosition; // Individual glyphs location - doesn't change
        private IntRect GlyphArea; // Total glyph-occupied region - doesn't change

        public bool IsExpired { get; private set; }

        public GlyphStreamNoise(GlyphStreamConfig settings, Rectangle workingArea)
        {
            _glyphs = new List<Glyph>();
            _workingArea = workingArea;

            _movementRate = GetRandom.Float(settings.MinMovementRate, settings.MaxMovementRate);            
            _scale = GetRandom.Float(settings.MinGlyphScale, settings.MaxGlyphScale);            
            var glyphCount = GetRandom.Int(settings.MinGlyphs, settings.MaxGlyphs);
            float displayDurationMultipier = GetRandom.Float(0.5f, 2f);
            
            var glyphSize = new Vector2f(Glyph.GLYPH_WIDTH * _scale, Glyph.GLYPH_HEIGHT * _scale);
            
            _maskSize = new Vector2f(glyphSize.X, glyphSize.Y * _glyphs.Count * displayDurationMultipier); // TODO: incorporate margin

            GlyphPosition = new Vector2f(
                GetRandom.Int((int)-glyphSize.X, (int)(_workingArea.Width + glyphSize.X)),
                GetRandom.Int((int)-glyphSize.Y, (int)(_workingArea.Height + glyphSize.Y)));

            GlyphArea = new IntRect(
                (int)GlyphPosition.X,
                (int)GlyphPosition.Y,
                (int)glyphSize.X,
                (int)glyphSize.Y + (int)(glyphSize.Y * settings.MarginScale * (_glyphs.Count - 1f))
                );            

            for (int i = 0; i < glyphCount; i++)
            {
                var y = GlyphPosition.Y + (i * Glyph.GLYPH_HEIGHT * _scale * settings.MarginScale);

                if (y + Glyph.GLYPH_HEIGHT < 0) continue;
                if (y > workingArea.Height) continue;

                _glyphs.Add(new Glyph(new Vector2f(GlyphPosition.X, y), _scale, settings.GlyphConfig));
            }

            MaskPosition = new Vector2f(GlyphPosition.X, GlyphPosition.Y - _maskSize.Y);                       
        }

        private IntRect MaskArea()
        {
            return new IntRect(
                (int)MaskPosition.X,
                (int)MaskPosition.Y,
                (int)_maskSize.X,
                (int)_maskSize.Y
                );
        }

        public void Render(RenderTarget canvas)
        {
            _glyphs.ForEach(g => g.Render(canvas));
        }

        public void Update(ChronoEventArgs chronoArgs)
        {
            if (IsExpired) return;

            MaskPosition.Y += (float)(_movementRate * chronoArgs.Delta);
            _glyphs.ForEach(g => g.Update(chronoArgs, MaskArea()));

            CheckIfExpired();
        }

        private void CheckIfExpired()
        {
            if (MaskPosition.Y > _workingArea.Bottom ||
                MaskPosition.Y > GlyphPosition.Y + GlyphArea.Height)
            {
                IsExpired = true;
            }
        }
    }
}