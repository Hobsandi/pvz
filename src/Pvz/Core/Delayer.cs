namespace Application
{
    public class Delayer(IUpdater updater, int ticksPerUpdate) : IUpdater
    {
        int ticksSinceUpdate = 0;

        public void Update()
        {
            ticksSinceUpdate++;
            if (ticksSinceUpdate < ticksPerUpdate) return;

            ticksSinceUpdate = 0;
            updater.Update();
        }

        public bool IsAlive() => updater.IsAlive();
    }
}
