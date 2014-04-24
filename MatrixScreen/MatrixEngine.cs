using FerretLib.SFML;
using SFML.Graphics;

namespace MatrixScreen
{
    public class MatrixEngine : IWorldEngine
    {
        private ViewPortCollection _viewports;        

        public MatrixEngine()
        {            
        }

        #region IWorldEngine
        void IWorldEngine.Render()
        {
            foreach (var viewport in _viewports)
            {
                viewport.Window.Clear(Color.Black);
                viewport.Window.Display();
            }
        }

        void IWorldEngine.Update()
        {
            
        }

        void IWorldEngine.Initialise(ViewPortCollection viewports)
        {
            _viewports = viewports;
        }
        #endregion
        
    }
}
