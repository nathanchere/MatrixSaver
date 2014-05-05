using System;

namespace MatrixScreen
{
    public class GlyphStreamConfig
    {
        public int MaxGlyphs { get; set; }
        public int MinGlyphs { get; set; }
        public float MaxMovementRate { get; set; }
        public float MinMovementRate { get; set; }
        public float MaxGlyphScale { get; set; }
        public float MinGlyphScale{ get; set; }

        /// <summary>
        /// 1 for normal; lower for glyphs closer together vertically
        /// </summary>
        public float MarginScale { get; set; }

        public GlyphConfig GlyphConfig { get; set; }
    }
}