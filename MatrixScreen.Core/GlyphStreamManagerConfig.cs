using System.Data;

namespace MatrixScreen
{
    public class GlyphStreamManagerConfig
    {
        public const string SHADER_GLITCH = @"GLITCH";
        public const string SHADER_GHOST = @"GHOST";

        public GlyphStreamConfig GlyphStreamConfig { get; private set; }

        public int MaximumGlyphStreams;
        public float ChanceOfNewGlyphStream;
        public string ShaderType;

        internal static GlyphStreamManagerConfig Debug()
        {
            return new GlyphStreamManagerConfig()
            {
                MaximumGlyphStreams = 1,
                ChanceOfNewGlyphStream = 1,

                GlyphStreamConfig = new GlyphStreamConfig()
                {
                    MaxGlyphs = 20,
                    MinGlyphs = 3,
                    MaxMovementRate = 300f,
                    MinMovementRate = 40f,

                    MinGlyphScale = 0.05f,
                    MaxGlyphScale = 0.4f,

                    GlyphConfig = GlyphConfig.Default(),
                },
            };
        }

        internal static GlyphStreamManagerConfig Mini()
        {
            return new GlyphStreamManagerConfig()
            {
                MaximumGlyphStreams = 800,
                ChanceOfNewGlyphStream = 0.31f,
                ShaderType = SHADER_GHOST,
                GlyphStreamConfig = new GlyphStreamConfig(){
                    MaxGlyphs = 20,
                    MinGlyphs = 3,
                    MaxMovementRate = 300f,
                    MinMovementRate = 40f,

                    MinGlyphScale= 0.02f,
                    MaxGlyphScale = 0.055f,
                    MarginScale = 0.8f,

                    GlyphConfig = GlyphConfig.Default(),
                },
            };
        }        

        internal static GlyphStreamManagerConfig Big()
        {
            return new GlyphStreamManagerConfig()
            {
                MaximumGlyphStreams = 50,
                ChanceOfNewGlyphStream = 0.03f,
                GlyphStreamConfig = new GlyphStreamConfig()
                {
                    MaxGlyphs = 20,
                    MinGlyphs = 3,
                    MaxMovementRate = 300f,
                    MinMovementRate = 40f,

                    MinGlyphScale = 0.02f,
                    MaxGlyphScale = 0.36f,
                    MarginScale = 0.8f,

                    GlyphConfig = GlyphConfig.Bright(),
                },
            };
        }

        internal static GlyphStreamManagerConfig BigFew()
        {
            return new GlyphStreamManagerConfig()
            {
                MaximumGlyphStreams = 6,
                ChanceOfNewGlyphStream = 0.09f,
                ShaderType = SHADER_GLITCH,

                GlyphStreamConfig = new GlyphStreamConfig()
                {
                    MaxGlyphs = 20,
                    MinGlyphs = 3,
                    MaxMovementRate = 300f,
                    MinMovementRate = 30f,

                    MinGlyphScale = 0.08f,
                    MaxGlyphScale = 0.2f,
                    MarginScale = 0.8f,

                    GlyphConfig = GlyphConfig.Bright(),
                },
            };
        }
    }
}