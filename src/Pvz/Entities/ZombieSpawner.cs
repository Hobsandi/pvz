using static Application.GameConfig;

namespace Application
{
    // Registreras med 1 tick och räknar själv, eftersom Delayers
    // intervall är fast och settings.Interval ändras under rundan.
    public class ZombieSpawner(Board board, SpawnSettings settings) : IUpdater
    {
        int ticks = 0;

        public void Update()
        {
            if (++ticks < settings.Interval) return;
            ticks = 0;

            // Olika banor, så att en våg inte staplas på samma ruta
            IEnumerable<int> lanes = Enumerable.Range(0, board.LaneCount)
                .OrderBy(_ => Random.Shared.Next())
                .Take(settings.ZombiesPerWave);
            foreach (int lane in lanes)
            {
                Position start = new(RIGHT_EDGE, lane);
                Zombie zombie = settings.Zombies.Pick().Create(start, settings.Health);
                board.Add(zombie, ZOMBIE_TICKS);
            }
        }

        public bool IsAlive() => true;
    }
}
