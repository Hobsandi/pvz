namespace Application
{
    // Går i vanlig takt men tål tre gånger så mycket
    public class TankZombieFactory(int health, PlayerBase playerBase, KillCounter killCounter) : IZombieFactory
    {
        public Zombie GenerateZombie(Position position)
            => new Zombie(position, health * 3, new WalkingMovement(), new SymbolSprite('T'), playerBase, killCounter);
    }
}
