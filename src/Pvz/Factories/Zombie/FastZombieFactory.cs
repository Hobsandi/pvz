namespace Application
{
    // Springer dubbelt så fort men tål bara hälften så mycket
    public class FastZombieFactory(PlayerBase playerBase, KillCounter killCounter) : IZombieFactory
    {
        public Zombie GenerateZombie(Position position, int health)
            => new Zombie(position, health / 2, new RunningMovement(), new SymbolSprite('F', ConsoleColor.Yellow), playerBase, killCounter);
    }
}
