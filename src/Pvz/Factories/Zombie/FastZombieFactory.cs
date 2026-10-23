namespace Application
{
    // Springer dubbelt så fort men tål bara hälften så mycket
    public class FastZombieFactory(int health, PlayerBase playerBase, KillCounter killCounter) : IZombieFactory
    {
        public Zombie GenerateZombie(Position position)
            => new Zombie(position, health / 2, new RunningMovement(), new SymbolSprite('F'), playerBase, killCounter);
    }
}
