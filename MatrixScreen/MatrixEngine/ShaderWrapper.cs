using System;
using SFML.Graphics;
using SFML.Window;

namespace MatrixScreen
{
    public abstract class ShaderWrapper
    {        
        public Shader Shader { get; protected set; }
        public abstract RenderStates Bind(RenderTexture canvas);
    }

    public class GlitchShader : ShaderWrapper
    {
        public GlitchShader()
        {
            Shader = new Shader(null, @"data/frag.c");     
        }

        public override RenderStates Bind(RenderTexture canvas)
        {
            var result = RenderStates.Default;

            if (DateTime.Now.Millisecond > 700)
            {
                Shader.SetParameter("texture", canvas.Texture);
                Shader.SetParameter("sigma", 0.5f);
                Shader.SetParameter("glowMultiplier", Mouse.GetPosition().X);
                Shader.SetParameter("width", Mouse.GetPosition().Y); // 1 = horizontal lines, up to around 1000 is good
                result.Shader = Shader;
            }
            return result;
        }
    } 
}