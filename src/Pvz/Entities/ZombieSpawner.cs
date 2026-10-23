using static Application.GameConfig;

namespace Application
{
    // Factory instead?
    public class ZombieSpawner(
        Board board, 
        PlayerBase playerBase, 
        Random random,
        int zombieTicks,
        Difficulty difficulty) : IUpdater
    {
        public void Update()
        {
            Zombie zombie = difficulty.GenerateZombie(board.RandomZombiePosition(random));
            board.Add(zombie, zombieTicks);
        }

        public bool IsAlive() => true;
    }
}
