namespace Application
{
    public class RegularZombieFactory(int health, PlayerBase playerBase, KillCounter killCounter) : IZombieFactory
    {
        public Zombie GenerateZombie(Position position)
            => new Zombie(position, health, new WalkingMovement(), new SymbolSprite('Z'),playerBase, killCounter);
    }
}