namespace Application
{
    public interface IWorld
    {
        void Register(IUpdater updater, int ticksPerUpdate);
    }
}
