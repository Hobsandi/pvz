namespace Application
{
    // En fabrik som använder de andra zombiefabrikerna och blir svårare med
    // tiden: den lyssnar på kills, ger zombierna mer hälsa och gör svårare
    // sorter vanligare. Spawnern behöver bara säga var zombien ska stå.
    public class ScalingZombieFactory : IKillObserver
    {
        private readonly int startingZombieHealth;
        private readonly WeightedTable<IZombieFactory> zombieFactories;
        private readonly IZombieFactory regular;
        private readonly IZombieFactory fast;
        private readonly IZombieFactory tank;
        private readonly IZombieFactory michael;
        private double multiplier = 1.0;

        public ScalingZombieFactory(int startingZombieHealth, PlayerBase playerBase,
            KillCounter killCounter, Random rand)
        {
            this.startingZombieHealth = startingZombieHealth;
            regular = new RegularZombieFactory(playerBase, killCounter);
            fast = new FastZombieFactory(playerBase, killCounter);
            tank = new TankZombieFactory(playerBase, killCounter);
            michael = new MichaelZombieFactory(playerBase, killCounter);

            // I början kommer bara vanliga zombies
            zombieFactories = new WeightedTable<IZombieFactory>(rand);
            zombieFactories.Add(regular, 1);
            zombieFactories.Add(fast, 0);
            zombieFactories.Add(tank, 0);
            zombieFactories.Add(michael, 0);
        }

        public void OnKill(int kills)
        {
            switch (kills) {
                case 10:
                    multiplier = 1.1;
                    zombieFactories.SetWeight(fast, 1);
                    break;
                case 15:
                    zombieFactories.SetWeight(michael, 1);
                    break;
                case 20:
                    multiplier = 1.2;
                    zombieFactories.SetWeight(tank, 1);
                    break;
                case 30:
                    multiplier = 1.3;
                    zombieFactories.SetWeight(fast, 2);
                    zombieFactories.SetWeight(tank, 2);
                    break;
                // Efter 30: +0.1 var tionde kill (40, 50, 60...)
                case > 30 when kills % 10 == 0:
                    multiplier += 0.1;
                    zombieFactories.AddWeight(michael, 1);
                    break;
            }
        }

        public Zombie GenerateZombie(Position position)
            => zombieFactories.Pick().GenerateZombie(position,
                (int)(startingZombieHealth * multiplier));
    }
}
