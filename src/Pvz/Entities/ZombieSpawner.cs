using static Application.GameConfig;

namespace Application
{
    // Bestämmer när och var. Vad som spawnas bestämmer fabriken.
    public class ZombieSpawner(
        Board board,
        Random random,
        int zombieTicks,
        ScalingZombieFactory zombieFactory) : IUpdater
    {
        public void Update()
        {
            Zombie zombie = zombieFactory.GenerateZombie(board.RandomZombiePosition(random));
            board.Add(zombie, zombieTicks);
        }

        public bool IsAlive() => true;
    }
}
