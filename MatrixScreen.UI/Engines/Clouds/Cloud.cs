using MatrixScreen;
using SFML.Graphics;

namespace FerretSS.Engines
{
    public class Cloud
    {        
        private float Speed;


        public Shape Shape;

        public Cloud(System.Drawing.Rectangle workingArea)
        {
            Speed = GetRandom.Float(0.02f, 0.5f);
            var alphaMod = 0.3f + Speed;
            
            Shape = new RectangleShape(GetRandom.Vector2f(40,400,30,200));
            //Shape = new CircleShape(GetRandom.Float(20,800));
            Shape.FillColor = new Color(255, 255, 255, (byte)(255 * alphaMod));
            Shape.Position = GetRandom.Vector2f(200,500,10,400);
        }

        public void Update()
        {
            Shape.Transform.Translate(-Speed*0.6f, 0);
        }    

        public bool Expired { get; private set; }
    }
}