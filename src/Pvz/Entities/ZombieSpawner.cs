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
        private IZombieFactory zombieFactory;
        /*
        public void SetFactory(IZombieFactory newFactory)
        {
            zombieFactory = newFactory;
        }
        */
        
        public void Update()
        {
            zombieFactory = difficulty.GetZombieFactory();
            Zombie zombie = zombieFactory.GenerateZombie(board.RandomZombiePosition(random));
            board.Add(zombie, zombieTicks);
        }

        public bool IsAlive() => true;
    }
}
