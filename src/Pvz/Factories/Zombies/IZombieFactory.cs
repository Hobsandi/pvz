namespace Application
{
    public interface IZombieFactory
    {
        Zombie Create(Position start, int health);
    }
}
