namespace Application
{
    public class Difficulty(KillCounter killCounter, int startingZombieHealth, PlayerBase playerBase)
    {
        
        private IZombieFactory zombieFactory;

        public IZombieFactory GetZombieFactory()
        {
            if (killCounter.Kills < 10)
                return new RegularZombieFactory(startingZombieHealth, playerBase, killCounter);
            else if (killCounter.Kills < 20)
                return new FastZombieFactory(startingZombieHealth, playerBase, killCounter);
            else
                return new TankZombieFactory(startingZombieHealth, playerBase, killCounter);
        }
    }
}