namespace FerretLib.SFML
{
    public interface IWorldEngine
    {
        void Render();
        void Update();
        void Initialise(ViewPortCollection viewports);
    }
}