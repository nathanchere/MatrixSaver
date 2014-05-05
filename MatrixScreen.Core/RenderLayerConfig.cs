namespace MatrixScreen
{
    public class RenderLayerConfig
    {
        public int MaximumGlyphStreams;
        public float ChanceOfNewGlyphStream;
        public float GlyphScaleMinimum;
        public float GlyphScaleMaximum;

        internal static RenderLayerConfig Debug()
        {
            return new RenderLayerConfig()
            {
                MaximumGlyphStreams = 1,
                ChanceOfNewGlyphStream = 1,
                GlyphScaleMinimum = 0.05f,
                GlyphScaleMaximum = 0.4f,
            };
        }

        internal static RenderLayerConfig Mini()
        {
            return new RenderLayerConfig()
            {
                MaximumGlyphStreams = 1000,
                ChanceOfNewGlyphStream = 0.08f,
                GlyphScaleMinimum = 0.02f,
                GlyphScaleMaximum = 0.08f,
            };
        }

        internal static RenderLayerConfig Big()
        {
            return new RenderLayerConfig()
            {
                MaximumGlyphStreams = 50,
                GlyphScaleMinimum = 0.02f,
                GlyphScaleMaximum = 0.36f,
                ChanceOfNewGlyphStream = 0.03f,
            };
        }
    }
}