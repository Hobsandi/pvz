namespace Application
{
    public class PoisonTicker(Zombie zombie, int damagePerDose, int doses)
        : IUpdater
    {
        int dosesLeft = doses;

        public void Update()
        {
            zombie.TakeDamage(damagePerDose);
            dosesLeft--;
        }

        public bool IsAlive() => dosesLeft > 0 && zombie.IsAlive();
    }
}
