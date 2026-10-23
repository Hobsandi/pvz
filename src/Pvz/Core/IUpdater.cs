namespace Application
{
    //IsAlive borde vara eget gränssnitt
    public interface IUpdater
    {
        void Update();
        bool IsAlive();
    }
}
