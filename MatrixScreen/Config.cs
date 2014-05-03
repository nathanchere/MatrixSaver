using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FerretLib.SFML;

namespace MatrixScreen
{
    public static class Config
    {
        public const bool IsDebugRendering = true;
        public const bool IsDebugGlyphStreams = true;
        public static ViewPortSettings ViewPortSettings = new ViewPortSettings
        {
            IsFullscreen = Config.IsFullscreen,
            IsMultiMonitorEnabled = Config.IsMultipleMonitorEnabled,
        };

        public const bool IsFullscreen = false;
        public const bool IsMultipleMonitorEnabled = true;
        public const int MaximumGlyphStreams = 1; // 50;
    }
}
