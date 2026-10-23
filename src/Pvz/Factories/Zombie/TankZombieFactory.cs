namespace Application
{
    // Går i vanlig takt men tål tre gånger så mycket
    public class TankZombieFactory(PlayerBase playerBase, KillCounter killCounter) : IZombieFactory
    {
        public Zombie GenerateZombie(Position position, int health)
            => new Zombie(position, health * 3, new WalkingMovement(), new SymbolSprite('T', ConsoleColor.Red), playerBase, killCounter);
    }
}
