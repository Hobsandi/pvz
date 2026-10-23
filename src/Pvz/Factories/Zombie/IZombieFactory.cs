namespace Application
{
    public interface IZombieFactory
    {
        // health är grundhälsan just nu. Fabriken kan ändra den
        // (t.ex. Tank tål mer) innan zombien skapas.
        Zombie GenerateZombie(Position position, int health);
    }
}
