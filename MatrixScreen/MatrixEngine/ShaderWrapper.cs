using SFML.Graphics;
using SFML.Window;

namespace MatrixScreen
{
    public abstract class ShaderWrapper
    {        
        public Shader Shader { get; protected set; }
        public abstract void Bind(RenderTexture canvas);
    }

    public class GlitchShader : ShaderWrapper
    {
        public GlitchShader()
        {
            Shader = new Shader(null, @"data/frag.c");     
        }

        public override void Bind(RenderTexture canvas)
        {
            var val = Mouse.GetPosition().X + 20;
            if (val < 300)
            {                
                Shader.SetParameter("texture", canvas.Texture);
                Shader.SetParameter("sigma", Mouse.GetPosition().Y);
                Shader.SetParameter("glowMultiplier", Mouse.GetPosition().X);
                Shader.SetParameter("width", 20);
                Shader.SetParameter("pixel_threshold", 0.7f / (3 * val));
            }
        }
    } 
}