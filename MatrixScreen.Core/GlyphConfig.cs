namespace MatrixScreen
{
    public class GlyphConfig
    {
        public byte MinR { get; set; }
        public byte MaxR { get; set; }
        public byte MinG { get; set; }
        public byte MaxG { get; set; }
        public byte MinB { get; set; }
        public byte MaxB { get; set; }
        public byte MinA { get; set; }
        public byte MaxA { get; set; }
        
        public float BrightnessVariation { get; set; }

        public float LightFlickerAlphaMaxVariation { get; set; }
        public byte HeavyFlickerMaxAlpha { get; set; }
        public byte HeavyFlickerMinAlpha { get; set; }
        public float HeavyFickerAlphaVariation { get; set; }        

        public float ChanceOfHeavyFlicker { get; set; }
        // TODO: heavyflicker duration?

        public static GlyphConfig Default()
        {
            return new GlyphConfig {
                MinR = 0,
                MaxR = 128,
                MinG = 224,
                MaxG = 255,
                MinB = 0,
                MaxB = 64,
                MinA = 160,
                MaxA = 255,
                BrightnessVariation = 0.2f,
                LightFlickerAlphaMaxVariation = 0.1f,
                HeavyFlickerMinAlpha = 32,
                HeavyFlickerMaxAlpha = 128,
                HeavyFickerAlphaVariation = 0.1f,

                ChanceOfHeavyFlicker = 0.01f,
            };
        }
    }
}