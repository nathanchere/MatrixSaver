using System;
using System.Configuration;
using System.Net.Configuration;
using FerretLib.SFML;
using SFML.Graphics;
using SFML.Window;

namespace MatrixScreen
{
    public class Glyph
    {
        public const int MAX_INDEX = 92; // maximum glyph character index

        public const int GLYPH_TEXTURE_COLUMNS = 16;
        public const int GLYPH_TEXTURE_ROWS = 8;

        public const int GLYPH_TEXTURE_SIZE = 2048;
        public const int GLYPH_WIDTH = GLYPH_TEXTURE_SIZE / GLYPH_TEXTURE_COLUMNS;
        public const int GLYPH_HEIGHT = GLYPH_TEXTURE_SIZE / GLYPH_TEXTURE_ROWS;

        private readonly GlyphConfig _config;

        private bool _isFlickering;

        private readonly TwitchCalculator _twitch;
        private readonly Sprite _sprite;
        private readonly IntRect _glyphArea;
        private static readonly Texture _texture = new Texture(@"data\glyphs.png") {
            Smooth = true,
            Repeated = false,
        };

        private int _index;
        private int Index
        {
            get
            {
                return _index;
            }
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

        public Glyph(Vector2f location, float scale, GlyphConfig settings)
        {
            _config = settings;

            _sprite = new Sprite(_texture)
            {
                Scale = new Vector2f(scale, scale),
                Position = location,
            };

            var glyphAreaX = (GLYPH_WIDTH*scale);
            _glyphArea = new IntRect(
                (int) (location.X - 0.5f*glyphAreaX),
                (int) location.Y,
                (int) glyphAreaX,
                (int) (GLYPH_HEIGHT*scale));

            //_sprite.Origin = new Vector2f(GLYPH_WIDTH * 0.5f * scale, 0);

            Index = GetRandom.Int(MAX_INDEX);
            _twitch = new TwitchCalculator();
        }

        public void Render(RenderTarget target)
        {
            if (!_isDraw) return;

            //// TODO: refactor to a debugGlyph class
            //if (Config.IsDebugRendering)
            //{
            //    Debug.DrawRect(target, new Color(255, 255, 0, 30), _sprite.Position.X,
            //        _sprite.Position.Y, _sprite.TextureRect.Width*_sprite.Scale.X,
            //        _sprite.TextureRect.Height*_sprite.Scale.Y, 0, 0);
            //}

            _sprite.Draw(target, new RenderStates(BlendMode.Alpha));
            _sprite.Draw(target, new RenderStates(BlendMode.Add));
        }

        public void Update(ChronoEventArgs chronoArgs, IntRect visibleRegion)
        {
            var modifier = GetVisibility(visibleRegion);

            _isDraw = modifier > 0;
            if(!_isDraw) return;
            
            if(GetRandom.Float(1f) < _config.ChanceOfHeavyFlicker) _isFlickering = !_isFlickering;

            _sprite.Color = new Color(0, 255, 0, CalculateOpacity());

            if (_twitch.IsTriggered(chronoArgs)) {
                Index = GetRandom.Int(MAX_INDEX);
            }
        }

        private byte CalculateOpacity()
        {
            return _isFlickering 
                ? GetRandom.Byte(_config.HeavyFlickerMinAlpha, _config.HeavyFlickerMinAlpha)
                : GetRandom.Byte(_config.MinA, _config.MaxA);
        }

        private float GetVisibility(IntRect visibleRegion)
        {
            // Outside bounds
            if (visibleRegion.Top > _glyphArea.Bottom())
                return 0;
            if (visibleRegion.Bottom() < _glyphArea.Top)
                return 0;

            // Completely within bounds
            if (visibleRegion.Top < _glyphArea.Top
                && visibleRegion.Bottom() > _glyphArea.Bottom())
                return 1;;


            // Partially within bounds - fading out
            if (visibleRegion.Top > _glyphArea.Top)
                return (_glyphArea.Bottom() - visibleRegion.Top) / (float)(_glyphArea.Bottom() - _glyphArea.Top);


            // Partially within bounds - fading in
            return (float)(visibleRegion.Bottom() - _glyphArea.Top) / _glyphArea.Height;
        }
    }
}