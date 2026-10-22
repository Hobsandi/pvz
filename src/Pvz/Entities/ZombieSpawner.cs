using static Application.GameConfig;

namespace Application
{
    // Factory instead?
    public class ZombieSpawner(
        Board board, PlayerBase playerBase, Random random) : IUpdater
    {
        public void Update()
        {
            Position start = new(RIGHT_EDGE, random.Next(ROWS));
            Zombie zombie = random.Next(4) == 0
                ? new Zombie(start, ZOMBIE_HEALTH,
                    new TeleportingMovement(random, 5),
                    new SymbolSprite('T'), playerBase)
                : new Zombie(start, ZOMBIE_HEALTH,
                    new MichaelMovement(),
                    new SymbolSprite('M'), playerBase);
            board.Add(zombie, ZOMBIE_TICKS);
        }

        public bool IsAlive() => true;
    }
}
