using System.Collections.Generic;

namespace MatrixScreen
{

    /// <summary>
    /// TODO:
    /// Configure keyboard exit
    /// Configure mouse exit threshold
    /// </summary>
    public static class ConfigProvider
    {
        public static MatrixConfig GetConfig()
        {
            var result = GetDevConfig();
            result.FpsLimit = 60;
            result.IsFullscreen = true;
            return result;
        }

        public static MatrixConfig GetDevConfig()
        {
            var smallConfigGlitch = GlyphStreamManagerConfig.Mini();
            smallConfigGlitch.MaximumGlyphStreams = 100;
            smallConfigGlitch.ShaderType = GlyphStreamManagerConfig.SHADER_GLITCH;

            var bigConfigNoGlitch = GlyphStreamManagerConfig.BigFew();
            bigConfigNoGlitch.ShaderType = null;
            var bigConfigGlitch = GlyphStreamManagerConfig.BigFew();
            bigConfigGlitch.MaximumGlyphStreams = 2;
            var bigConfigGhost = GlyphStreamManagerConfig.BigFew();
            bigConfigGhost.MaximumGlyphStreams = 2;
            bigConfigGhost.ShaderType = GlyphStreamManagerConfig.SHADER_GHOST;
                 
            return new MatrixConfig
            {   
                FpsLimit = 60,
                IsFullscreen = true,
                IsMultipleMonitorEnabled = true,
                RenderLayers = new List<GlyphStreamManagerConfig>{
                    smallConfigGlitch,
                    smallConfigGlitch,
                    smallConfigGlitch,
                    smallConfigGlitch,
                    smallConfigGlitch,
                    bigConfigNoGlitch,
                    bigConfigGlitch,
                    bigConfigGlitch,
                    bigConfigGlitch,
                    bigConfigGhost,
                },
            };
        }

        public static MatrixConfig GetNormalConfig()
        {
            return new MatrixConfig
            {
                FpsLimit = 60,
                IsFullscreen = true,
                IsMultipleMonitorEnabled = true,

                RenderLayers = new List<GlyphStreamManagerConfig>{
                    GlyphStreamManagerConfig.Big(),
                },
            };
        }

        public static MatrixConfig GetDebugConfig()
        {
            return new MatrixConfig
            {
                FpsLimit = 60,
                IsFullscreen = false,
                IsMultipleMonitorEnabled = false,

                RenderLayers = new List<GlyphStreamManagerConfig>{
                    GlyphStreamManagerConfig.Debug(),
                },
            };
        }
    }
}