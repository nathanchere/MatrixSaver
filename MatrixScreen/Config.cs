using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FerretLib.SFML;

namespace MatrixScreen
{
    public static class Config
    {
        public const bool IsDebugRendering = false;
        public static ViewPortSettings ViewPortSettings = new ViewPortSettings
        {
            IsFullscreen = false,
            IsMultiMonitorEnabled = true,
        };
    }
}
