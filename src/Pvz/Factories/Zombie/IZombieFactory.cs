namespace Application
{
    public interface IZombieFactory
    {
        Zombie GenerateZombie(Position position);
    }
}