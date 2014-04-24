namespace FerretLib.SFML.Utility
{
    public interface IWorldEngine
    {
        void Render();
        void Update();
        void Initialise(ViewPortCollection viewports);
    }
}