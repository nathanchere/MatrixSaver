using System;
using System.Configuration;
using FerretLib.SFML;
using SFML.Graphics;
using SFML.Window;

namespace MatrixScreen
{
    public class Glyph // :IEntity // - need to rethink this
    {
        public const int MAX_INDEX = 92; // maximum glyph character index

        public const int GLYPH_TEXTURE_COLUMNS = 16;
        public const int GLYPH_TEXTURE_ROWS = 8;

        public const int GLYPH_TEXTURE_SIZE = 2048;
        public const int GLYPH_WIDTH = GLYPH_TEXTURE_SIZE / GLYPH_TEXTURE_COLUMNS;
        public const int GLYPH_HEIGHT = GLYPH_TEXTURE_SIZE / GLYPH_TEXTURE_ROWS;

        private readonly TwitchCalculator _twitch;
        private readonly Sprite _sprite;
        private readonly IntRect _glyphArea;
        private static readonly Texture _texture = new Texture(@"data\glyphs.png")
        {
            Smooth = true,
            Repeated = false,
        };

        private int _index;
        private int Index
        {
            get { return _index; }
            set
            {
                _index = value;
                var x = value % GLYPH_TEXTURE_COLUMNS;
                var y = (value - x) / GLYPH_TEXTURE_COLUMNS;
                _sprite.TextureRect = new IntRect(
                    x * GLYPH_WIDTH,
                    y * GLYPH_HEIGHT,
                    GLYPH_WIDTH,
                    GLYPH_HEIGHT
                );
            }
        }

        bool _isDraw = false;

        public Glyph(Vector2f location, float scale)
        {
            _sprite = new Sprite(_texture)
            {
                Origin = new Vector2f(GLYPH_WIDTH * 0.5f, 0),
                Scale = new Vector2f(scale, scale),
                Position = location
            };

            var glyphAreaX = (GLYPH_WIDTH * scale);
            _glyphArea = new IntRect(
                (int)location.X - (int)(0.5f * glyphAreaX),
                (int)location.Y,
                (int)glyphAreaX,
                (int)(GLYPH_HEIGHT * scale));

            Index = GetRandom.Int(MAX_INDEX);
            _twitch = new TwitchCalculator();
        }

        public void Render(RenderTarget target)
        {
            if (!_isDraw) return;

            if (_glyphArea.Contains(Mouse.GetPosition().X, Mouse.GetPosition().Y))
            {
                _sprite.Color = new Color(255, 170, 170);
                var x = new RectangleShape(new Vector2f(_glyphArea.Width, _glyphArea.Height));
                x.Position = new Vector2f(_glyphArea.Left, _glyphArea.Top);
                x.FillColor = new Color(255,255,0,30);
                x.Draw(target,RenderStates.Default);
            }

            _sprite.Draw(target, RenderStates.Default);            
        }

        public void Update(ChronoEventArgs chronoArgs, IntRect visibleRegion)
        {
            var modifier = GetVisibility(visibleRegion);
            
            _isDraw = modifier > 0;

            _sprite.Color = new Color(0, 255, 0, (byte)(190 * modifier));            

            if (_twitch.IsTriggered(chronoArgs))
            {
                Index = GetRandom.Int(MAX_INDEX);
            }
        }

        private float GetVisibility(IntRect visibleRegion)
        {
            // Outside bounds
            if (visibleRegion.Top > _glyphArea.Bottom()) return 0;
            if (visibleRegion.Bottom() < _glyphArea.Top) return 0;

            // Completely within bounds
            if (visibleRegion.Top < _glyphArea.Bottom()
                && visibleRegion.Bottom() > _glyphArea.Top)
                return 1;

            return 1;

            // Partially within bounds - fading in
            if (visibleRegion.Top > _glyphArea.Top) return 0;
            return visibleRegion.Top - (visibleRegion.Top - _glyphArea.Top) / _glyphArea.Height;

            // Partially within bounds - fading out
            //if (visibleRegion.Bottom() < _glyphArea.Top) return 0;
            return (visibleRegion.Bottom() - _glyphArea.Top) / _glyphArea.Height;
        }
    }
}