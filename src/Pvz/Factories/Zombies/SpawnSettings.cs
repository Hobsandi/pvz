using static Application.GameConfig;

namespace Application
{
    // Delas mellan ZombieSpawner (läser) och RunDirector (ändrar under rundan).
    public class SpawnSettings
    {
        public int Interval { get; set; } = SPAWN_TICKS;
        public int ZombiesPerWave { get; set; } = 1;
        public int Health { get; set; } = ZOMBIE_HEALTH;
        public WeightedTable<IZombieFactory> Zombies { get; } = new();
    }
}
