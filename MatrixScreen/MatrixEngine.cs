using System.Drawing;
using FerretLib.SFML;
using SFML.Graphics;
using SFML.Window;
using Color = SFML.Graphics.Color;
using Font = SFML.Graphics.Font;

namespace MatrixScreen
{
    public class MatrixEngine : IWorldEngine
    {
        private GlyphManager glyphs;

        private ChronoDisplay chrono;

        public MatrixEngine()
        {
            chrono = new ChronoDisplay();
        }

        #region Contracts
        public void Render(RenderTarget canvas)
        {
            var vertices = new []{
                new Vertex(new Vector2f(0,0), new Color(0,48,32)),
                new Vertex(new Vector2f(canvas.Size.X,0), new Color(0,0,32)),
                new Vertex(new Vector2f(canvas.Size.X,canvas.Size.Y), new Color(32,0,0)),
                new Vertex(new Vector2f(0,canvas.Size.Y), new Color(16,0,32)),
            };

            var verticesInner = new []{
                new Vertex(new Vector2f(10,10), new Color(0,0,30)),
                new Vertex(new Vector2f(canvas.Size.X-10,10), new Color(0,0,0)),
                new Vertex(new Vector2f(canvas.Size.X-10,canvas.Size.Y-10), new Color(30,0,30)),
                new Vertex(new Vector2f(10,canvas.Size.Y-10), new Color(0,0,0)),
            };
            canvas.Draw(vertices, PrimitiveType.Quads);
            canvas.Draw(verticesInner, PrimitiveType.Quads);

            chrono.Render(canvas);
            //glyphs.Draw(canvas);                    
        }

        public void Update(ChronoEventArgs chronoArgs)
        {
            chrono.Update(chronoArgs);
            glyphs.Update(chronoArgs.Delta);
        }

        void IWorldEngine.Initialise(ViewPortCollection viewports)
        {
            var area = new Vector2u(
                (uint)viewports.WorkingArea.Width,
                (uint)viewports.WorkingArea.Height);
            glyphs = new GlyphManager(area);
        }
        #endregion

    }
}
