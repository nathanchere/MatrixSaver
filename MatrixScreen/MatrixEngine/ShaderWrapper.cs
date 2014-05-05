using System;
using FerretLib.SFML;
using SFML.Graphics;
using SFML.Window;

namespace MatrixScreen
{
    public abstract class ShaderWrapper
    {        
        public Shader Shader { get; protected set; }
        public abstract RenderStates Bind(RenderTexture canvas);
        protected ChronoEventArgs chronoEvent;

        public static ShaderWrapper Get(string type)
        {
            if (type.ToUpperInvariant() == GlyphStreamManagerConfig.SHADER_GLITCH) return new GlitchShader();
            return null;
        }

        public void Update(ChronoEventArgs args)
        {
            chronoEvent = args;
        }

        private class GlitchShader : ShaderWrapper
        {
            public GlitchShader()
            {
                Shader = new Shader(null, @"data/frag.c");
            }

            public override RenderStates Bind(RenderTexture canvas)
            {
                // TODO - make more random and not quantised to seconds
                var result = RenderStates.Default;

                double milliseconds = chronoEvent.Monotonic % 3f;
                if (milliseconds < 0.18f)
                {
                    Shader.SetParameter("texture", canvas.Texture);
                    Shader.SetParameter("sigma", 2);
                    Shader.SetParameter("glowMultiplier", 800);
                    Shader.SetParameter("width", (float)((1 - milliseconds)* 1000f) * (float)milliseconds); // 1 = horizontal lines, up to around 1000 is good
                    result.Shader = Shader;
                }
                return result;
            }
        } 
    }
}