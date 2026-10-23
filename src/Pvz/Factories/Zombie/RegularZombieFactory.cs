namespace Application
{
    public class RegularZombieFactory(PlayerBase playerBase, KillCounter killCounter) : IZombieFactory
    {
        public Zombie GenerateZombie(Position position, int health)
            => new Zombie(position, health, new WalkingMovement(), new SymbolSprite('Z'), playerBase, killCounter);
    }
}
