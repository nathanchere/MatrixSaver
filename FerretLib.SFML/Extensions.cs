using System.Drawing;
using SFML.Graphics;
using SFML.Window;

namespace FerretLib.SFML
{
    public static class Extensions
    {
        #region Basic conversion
        public static Vector2i ToVector2i(this Vector2f input)
        {
            return new Vector2i((int)input.X, (int)input.Y);
        }

        public static Vector2f ToVector2f(this Vector2i input)
        {
            return new Vector2f(input.X, input.Y);
        }

        public static Point ToPoint(this Vector2i input)
        {
            return new Point(input.X, input.Y);
        }

        public static Point ToPoint(this Vector2f input)
        {
            return new Point((int)input.X, (int)input.Y);
        }

        public static Vector2f ToVector2f(this Size input)
        {
            return new Vector2f(input.Width, input.Height);
        }

        public static Vector2f ToVector2f(this Point input)
        {
            return new Vector2f(input.X, input.Y);
        }

        public static Vector2i ToVector2i(this Size input)
        {
            return new Vector2i(input.Width, input.Height);
        }

        public static Vector2i ToVector2i(this Point input)
        {
            return new Vector2i(input.X, input.Y);
        }

        public static Rectangle ToRectangle(this Vector2u input)
        {
            return new Rectangle(0, 0, (int)input.X, (int)input.Y);
        }

        public static IntRect ToIntRect(this Rectangle input)
        {
            return new IntRect(input.X,input.Y,input.Width,input.Height);
        }
        #endregion

        #region Rectangle helpers
        public static int Bottom(this IntRect input)
        {
            return input.Top + input.Height;
        }

        public static int Right(this IntRect input)
        {
            return input.Left + input.Width;
        }
        #endregion

        public static Rectangle Normalize(this Rectangle rectangle)
        {
            return new Rectangle(0,0,rectangle.Width,rectangle.Height);
        }
    }
}
