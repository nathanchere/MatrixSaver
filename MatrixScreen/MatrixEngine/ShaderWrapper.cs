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
            if (type.ToUpperInvariant() == GlyphStreamManagerConfig.SHADER_GHOST) return new GhostShader();
            return null;
        }

        public void Update(ChronoEventArgs args)
        {
            chronoEvent = args;
        }

        private class GlitchShader : ShaderWrapper
        {
            private double nextTick;
            private double tickDuration;

            public GlitchShader()
            {
                Shader = new Shader(null, @"data/frag.c");
                Tick(0);
            }

            private void Tick(double monotonic)
            {
                nextTick = monotonic + GetRandom.Double(0.8, 3); // TODO - change to much higher max value
                tickDuration = monotonic + GetRandom.Double(0.01, 0.07f);
            }


            public override RenderStates Bind(RenderTexture canvas)
            {
                var result = RenderStates.Default;

                if (chronoEvent.Monotonic > nextTick)
                {
                    Tick(chronoEvent.Monotonic);
                }

                var milliseconds = tickDuration - chronoEvent.Monotonic;
                if (milliseconds > 0)
                {                    
                    Shader.SetParameter("texture", canvas.Texture);
                    Shader.SetParameter("sigma", 2);
                    Shader.SetParameter("glowMultiplier", 800);
                    Shader.SetParameter("width", (float)((1 - milliseconds)* 1000f) * (float)milliseconds * 2f); // 1 = horizontal lines, up to around 1000 is good
                    result.Shader = Shader;
                }
                return result;
            }
        }

        private class GhostShader : ShaderWrapper
        {
            private double nextTick;
            private double tickDuration;

            public GhostShader()
            {
                Shader = new Shader(null, @"data/frag.c");
                Tick(0);
            }

            private void Tick(double monotonic)
            {
                nextTick = monotonic + GetRandom.Double(0.8, 3); // TODO - change to much higher max value
                tickDuration = monotonic + GetRandom.Double(0.01, 0.07f);
            }


            public override RenderStates Bind(RenderTexture canvas)
            {
                var result = RenderStates.Default;

                if (chronoEvent.Monotonic > nextTick)
                {
                    Tick(chronoEvent.Monotonic);
                }

                var milliseconds = tickDuration - chronoEvent.Monotonic;
                if (milliseconds > 0)
                {                    
                    Shader.SetParameter("texture", canvas.Texture);
                    Shader.SetParameter("sigma", Mouse.GetPosition().X);
                    Shader.SetParameter("glowMultiplier", 800);
                    Shader.SetParameter("width", Mouse.GetPosition().Y * 0.3f);
                    result.Shader = Shader;
                }
                return result;
            }
        }
    }
}