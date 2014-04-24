using FerretLib.SFML;
using FerretLib.SFML.Utility;
using FerretSS;
using SFML.Graphics;
using SFML.Window;
using System.Collections.Generic;
using System;
namespace FerretSS.Engines
{
    public class CloudsHandler : IWorldEngine
    {
        #region Internal variables     
        private ViewPortCollection _viewports;
        private List<Cloud> _clouds;

        public CloudsHandler()
        {            
        }
        #endregion

        #region IWorldEngine
        void IWorldEngine.Render()
        {
            foreach (var viewport in _viewports)
            {
                viewport.Window.Clear(new Color(64,128,255));
                _clouds.ForEach(x =>
                    {
                        viewport.Window.Draw(x.Shape);
                    });
                viewport.Window.Display();
            }
        }

        void IWorldEngine.Update()
        {
            _clouds.ForEach(x => x.Update());
        }

        void IWorldEngine.Initialise(ViewPortCollection viewports)
        {
            _viewports = viewports;

            _clouds = new List<Cloud>();
            for (int i = 0; i < 20; i++)
                _clouds.Add(new Cloud(_viewports.WorkingArea));            
        }
        #endregion
    }
}
