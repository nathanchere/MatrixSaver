using System;
using FerretLib.SFML;
using SFML.Graphics;
using SFML.Window;
using Color = SFML.Graphics.Color;
using Font = SFML.Graphics.Font;

namespace MatrixScreen
{
    public class DebugEngine : IWorldEngine
    {
        private ViewPortCollection _viewports;
        private Text text;        

        public DebugEngine()
        {
            text = new Text
            {
                Font = new Font(@"data\lekton.ttf"),
                CharacterSize = 12,
                Color = Color.Green,
            };
        }

        #region IWorldEngine
        void IWorldEngine.Render()
        {
            var cursorPosition = _viewports.CursorPosition();
            var globalCursorText = string.Format("Global cursor: {0}:{1}", cursorPosition.X, cursorPosition.Y);
            foreach (var viewport in _viewports)
            {
                viewport.Window.Clear(Color.Black);

                text.Color = viewport.WorkingArea.Contains(Mouse.GetPosition().ToPoint()) ?
                    Color.Green : Color.Red;

                text.Position = new Vector2f(30, 30);
                text.DisplayedString = string.Format("Cursor: {0}:{1}", Mouse.GetPosition().X, Mouse.GetPosition().Y);
                viewport.Window.Draw(text);

                text.Position = new Vector2f(30, 50);
                text.DisplayedString = globalCursorText;
                viewport.Window.Draw(text);

                var pos = _viewports.GetLocalCoordinates(Mouse.GetPosition(),viewport);
                text.Position = new Vector2f(30, 70);
                text.DisplayedString = string.Format("Local position: {0}:{1}",
                    pos.X,pos.Y);
                viewport.Window.Draw(text);

                text.Position = new Vector2f(30, 90);
                text.DisplayedString = string.Format("Viewport #{0}; origin: {1},{2}",
                    viewport.ID,
                    viewport.WorkingArea.Left,
                    viewport.WorkingArea.Top);
                viewport.Window.Draw(text);

                text.Position = new Vector2f(30, 110);
                text.DisplayedString = string.Format("Global boundaries: t:{0},l:{1},r:{2},b{3}",
                    _viewports.WorkingArea.Top,
                    _viewports.WorkingArea.Left,
                    _viewports.WorkingArea.Right,
                    _viewports.WorkingArea.Bottom);
                viewport.Window.Draw(text);

                viewport.Window.Display();
            }            
        }

        void IWorldEngine.Update(ChronoEventArgs chronoArgs)
        {

        }

        void IWorldEngine.Initialise(ViewPortCollection viewports)
        {
            _viewports = viewports;
        }
        #endregion

    }
}
