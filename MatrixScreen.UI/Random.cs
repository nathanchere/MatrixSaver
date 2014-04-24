using SFML.Window;

namespace MatrixScreen
{
    public static class Random
    {
        private static System.Random _randomNumber;

        private static System.Random RandomNumber
        {
            get { _randomNumber = _randomNumber ?? new System.Random(); return _randomNumber; }
        }

        public static float Float(float min, float max)
        {
            return (float) Double(min, max);
        }

        public static double Double(double min, double max)
        {
            return (RandomNumber.NextDouble()*(max - min)) + min;
        }

        public static int Int(int min, int max)
        {
            return RandomNumber.Next(min, max);
        }

        public static byte Byte(byte min, byte max)
        {
            return (byte)Int(min, max);
        }

        public static Vector2f Vector2f(float minX, float maxX, float minY, float maxY)
        {
            return new Vector2f(
                Float(minX, maxX),
                Float(minY, maxY)
                );
        }

        public static Vector2i Vector2i(int minX, int maxX, int minY, int maxY)
        {
            return new Vector2i(
                Int(minX, maxX),
                Int(minY, maxY)
                );
        }
    }
}