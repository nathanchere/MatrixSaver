namespace FerretLib.SFML
{
    public interface IWorldEngine
    {
        void Render();
        void Update(ChronoEventArgs chronoArgs);
        void Initialise(ViewPortCollection viewports);
    }
}