namespace FerretLib.SFML
{
    public interface IEntity
    {
        void Render();
        void Update(ChronoEventArgs chonoArgs);

    }
}