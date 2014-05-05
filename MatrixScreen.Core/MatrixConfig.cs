using System.Collections.Generic;

namespace MatrixScreen
{
    public class MatrixConfig
    {
        public bool IsFullscreen;
        public bool IsMultipleMonitorEnabled;

        public int FpsLimit;

        public bool IsDebugRendering; //TODO: remove these
        public bool IsDebugGlyphStreams; //TODO: remove these

        public List<RenderLayerConfig> RenderLayers { get; set; }
    }
}
