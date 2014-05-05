using System.Collections.Generic;

namespace MatrixScreen
{
    public static class ConfigProvider
    {
        public static MatrixConfig GetConfig()
        {
            var result = GetNormalConfig();
            result.FpsLimit = 60;
            result.IsFullscreen = false;
            return result;
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

                RenderLayers = new List<RenderLayerConfig>(),
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

                RenderLayers = new List<RenderLayerConfig>{
                    RenderLayerConfig.Mini(),
                },
            };
        }
    }
}