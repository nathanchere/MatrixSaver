using FerretLib.SFML;
using SFML.Graphics;
using SFML.Window;

namespace MatrixScreen
{
    public class MatrixEngine : IWorldEngine
    {
        private ViewPortCollection _viewports;
        private Text text;

        public MatrixEngine()
        {
            text = new Text {
                Font = new Font(@"data\lekton.ttf"),                
                CharacterSize = 12,
                Color = Color.Green,
            };
        }

        #region IWorldEngine
        void IWorldEngine.Render()
        {
            foreach (var viewport in _viewports)
            {
                viewport.Window.Clear(Color.Black);
                text.Position = new Vector2f(30,30);
                text.DisplayedString = string.Format("{0}:{1}", Mouse.GetPosition().X, Mouse.GetPosition().Y);
                viewport.Window.Draw(text);
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
