using FerretLib.SFML;

namespace MatrixScreen
{
    public class Program
    {        

        private static void Main(string[] args)
        {
            var settings = ConfigProvider.GetConfig();
            var engineSettings = new ScreenSaverSettings() { 
                IsFullscreen = settings.IsFullscreen,
                IsMultiMonitorEnabled = settings.IsMultipleMonitorEnabled,
                MaxFps = settings.FpsLimit,
            };

            var screenSaver = new ScreenSaverEngine(engineSettings);
            screenSaver.Engine = new MatrixEngine(settings);
            screenSaver.BindEscapeToExit();
            screenSaver.Run();            
        }
    }
}
