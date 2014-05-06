using System.Collections.Generic;

namespace MatrixScreen
{
    public class MatrixConfig
    {
        public bool IsFullscreen;
        public bool IsMultipleMonitorEnabled;

        public int FpsLimit;

        public List<GlyphStreamManagerConfig> RenderLayers { get; set; }
    }
}
