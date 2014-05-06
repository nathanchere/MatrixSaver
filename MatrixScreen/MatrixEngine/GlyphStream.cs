using System.Collections.Generic;
using System.Drawing;
using FerretLib.SFML;
using SFML.Graphics;
using SFML.Window;

namespace MatrixScreen
{
    public class GlyphStream : IEntity
    {
        private readonly float _movementRate;
        private readonly float _scale;
        private readonly float _displayDurationMultipier; // affects how long 

        private readonly Rectangle _workingArea;
        private readonly List<Glyph> _glyphs;
        private readonly Vector2f MaskSize;
        private readonly Vector2f GlyphSize;        
       
        private Vector2f MaskPosition; // Stream position - scrolls down the screen
        private Vector2f GlyphPosition; // Individual glyphs location - doesn't change
        private IntRect GlyphArea; // Total glyph-occupied region - doesn't change

        public bool IsExpired { get; private set; }

        public GlyphStream(GlyphStreamConfig settings, Rectangle workingArea)
        {
            _workingArea = workingArea;

            var numberOfGlyphs = GetRandom.Int(settings.MinGlyphs, settings.MaxGlyphs);

            _movementRate = GetRandom.Float(settings.MinMovementRate, settings.MaxMovementRate);            
            _scale = GetRandom.Float(settings.MinGlyphScale, settings.MaxGlyphScale);
            _displayDurationMultipier = GetRandom.Float(0.5f / numberOfGlyphs, 1f);

            GlyphSize = new Vector2f(Glyph.GLYPH_WIDTH * _scale, Glyph.GLYPH_HEIGHT * _scale);

            GlyphPosition = new Vector2f(
                GetRandom.Int((int)-GlyphSize.X, (int)(_workingArea.Width + GlyphSize.X)),
                GetRandom.Int((int)-GlyphSize.Y, (int)(_workingArea.Height + GlyphSize.Y)));

            _glyphs = new List<Glyph>();
            for (int i = 0; i < numberOfGlyphs; i++)
            {
                var y = GlyphPosition.Y + (i * Glyph.GLYPH_HEIGHT * _scale * settings.MarginScale);

                if (y + Glyph.GLYPH_HEIGHT < 0) continue;
                if (y > workingArea.Height) continue;

                _glyphs.Add(new Glyph(new Vector2f(GlyphPosition.X, y), _scale, settings.GlyphConfig));
            }

            MaskPosition = new Vector2f(GlyphPosition.X, GlyphPosition.Y - MaskSize.Y);
            MaskSize = new Vector2f(GlyphSize.X, GlyphSize.Y * _glyphs.Count * _displayDurationMultipier); // TODO: incorporate margin
            
        
            GlyphArea = new IntRect(
                (int)GlyphPosition.X,
                (int)GlyphPosition.Y,
                (int)GlyphSize.X,
                (int)GlyphSize.Y + (int)(GlyphSize.Y * settings.MarginScale * (_glyphs.Count - 1f))
                );
        }

        private IntRect MaskArea()
        {
            return new IntRect(
                (int)MaskPosition.X,
                (int)MaskPosition.Y,
                (int)MaskSize.X,
                (int)MaskSize.Y
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