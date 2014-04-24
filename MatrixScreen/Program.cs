using FerretLib.SFML;

namespace MatrixScreen
{
    public class Program
    {        

        private static void Main(string[] args)
        {
            var screenSaver = new ScreenSaverEngine();
            screenSaver.Engine = new MatrixEngine();
            screenSaver.BindEscapeToExit();
            screenSaver.Run();            
        }
    }
}
