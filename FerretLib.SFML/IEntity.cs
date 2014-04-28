using SFML.Graphics;

namespace FerretLib.SFML
{
    public interface IEntity
    {
        void Render(RenderTarget target);
        void Update(ChronoEventArgs chronoArgs);
    }
}