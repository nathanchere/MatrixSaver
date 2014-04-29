using System;
using System.Configuration;
using FerretLib.SFML;
using SFML.Graphics;
using SFML.Window;

namespace MatrixScreen
{
    public class Glyph : IEntity
    {
        public const int MAX_INDEX = 92; // maximum glyph character index

        public const int GLYPH_TEXTURE_COLUMNS = 16;
        public const int GLYPH_TEXTURE_ROWS = 8;

        public const int GLYPH_TEXTURE_SIZE = 2048;
        public const int GLYPH_WIDTH = GLYPH_TEXTURE_SIZE / GLYPH_TEXTURE_COLUMNS;
        public const int GLYPH_HEIGHT = GLYPH_TEXTURE_SIZE / GLYPH_TEXTURE_ROWS;

        private float _chanceOfChange;
        private float _changeCounter;

        private readonly Sprite _sprite;
        private static readonly Texture _texture = new Texture(@"data\glyphs.png")
        {
            Smooth = true,
            Repeated = false,
        };

        private int _index;
        private int Index
        {
            get { return _index; }
            set { 
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

            Index = GetRandom.Int(MAX_INDEX);
            _chanceOfChange = GetRandom.Float(0.4f);
        }

        public void Render(RenderTarget target)
        {  
            _sprite.Color = new Color(0, 255, 0, 190);
            _sprite.Draw(target, RenderStates.Default);
            //}
            //else
            //{
            //    glyphSprite.Color = new Color(255, 255, 255, 5);
            //}            
        }

        public void Update(ChronoEventArgs chronoArgs)
        {
            _changeCounter += GetRandom.Float(1) * (float)chronoArgs.Delta;
            if (_changeCounter > 1f - _chanceOfChange)
            {
                _changeCounter = 0;
                Index = GetRandom.Int(MAX_INDEX);
            }
            // random change to change

            // isdraw = is contained in drawing area

            //_sprite.TextureRect = new IntRect(GLYPH_WIDTH * (int)(DateTime.Now.Second * 0.25f), ((int)(DateTime.Now.Millisecond * 0.008) % 4) * GLYPH_HEIGHT, GLYPH_WIDTH, GLYPH_HEIGHT);
        }
    }
}