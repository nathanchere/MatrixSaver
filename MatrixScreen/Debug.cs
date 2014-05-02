using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Window;

namespace MatrixScreen
{
    public static class Debug
    {
        private static readonly Text _text;
        private static readonly RectangleShape _rectangle;

        static Debug()
        {
            _rectangle = new RectangleShape();

            _text = new Text("", new Font(@"data/lekton.ttf")) {
                Color = Color.Yellow,
                CharacterSize = 14,
            };
        }

        public static void DrawRect(RenderTarget target, Color color, float x, float y, float width, float height, float originX, float originY)
        {
            _rectangle.Position = new Vector2f(x, y);
            _rectangle.Size = new Vector2f(width, height);
            _rectangle.FillColor = color;
            target.Draw(_rectangle, RenderStates.Default);
        }

        public static void DrawText(RenderTarget target, Color color, string text, float x, float y)
        {
            DrawText(target, color, text, new Vector2f(x, y));
        }

        public static void DrawText(RenderTarget target, Color color, string text, Vector2f position)
        {
            _text.Position = position;
            _text.Color = color;
            _text.DisplayedString = text;
            _text.Draw(target, RenderStates.Default);
        }
    }
}
