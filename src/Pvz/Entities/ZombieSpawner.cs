using static Application.GameConfig;

namespace Application
{
    // Factory instead?
    public class ZombieSpawner(
        Board board, PlayerBase playerBase, Random random,
        int zombieHealth, int zombieTicks) : IUpdater
    {
        public void Update()
        {
            Position start = new(RIGHT_EDGE, random.Next(ROWS));
            Zombie zombie = random.Next(4) == 0
                ? new Zombie(start, zombieHealth,
                    new TeleportingMovement(random, 5),
                    new SymbolSprite('T'), playerBase)
                : new Zombie(start, zombieHealth,
                    new MichaelMovement(),
                    new SymbolSprite('M'), playerBase);
            board.Add(zombie, zombieTicks);
        }

        public bool IsAlive() => true;
    }
}
