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
            return new MatrixConfig
            {
                FpsLimit = 60,
                IsFullscreen = true,
                IsMultipleMonitorEnabled = true,
                IsDebugRendering = false,
                IsDebugGlyphStreams = false,

                RenderLayers = new List<GlyphStreamManagerConfig>{
                    //GlyphStreamManagerConfig.Mini(),
                    GlyphStreamManagerConfig.BigFew(),
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
                IsDebugRendering = false,
                IsDebugGlyphStreams = false,

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
                IsDebugRendering = true,
                IsDebugGlyphStreams = true,

                RenderLayers = new List<GlyphStreamManagerConfig>{
                    GlyphStreamManagerConfig.Debug(),
                },
            };
        }
    }
}