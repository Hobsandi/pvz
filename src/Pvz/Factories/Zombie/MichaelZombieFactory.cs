namespace Application
{
    // Moonwalkar fram och tillbaka med MichaelMovement
    public class MichaelZombieFactory(PlayerBase playerBase, KillCounter killCounter) : IZombieFactory
    {
        public Zombie GenerateZombie(Position position, int health)
            => new Zombie(position, health, new MichaelMovement(), new SymbolSprite('M'), playerBase, killCounter);
    }
}
