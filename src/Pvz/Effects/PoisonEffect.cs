namespace Application
{
    public class PoisonEffect(
        IWorld world,
        int damagePerDose,
        int doses,
        int ticksBetweenDoses) : IEffect
    {
        public void Apply(Zombie zombie)
        {
            PoisonTicker ticker = new(zombie, damagePerDose, doses);
            world.Register(ticker, ticksBetweenDoses);
        }

        public string Describe() => $"poison {damagePerDose} x {doses}";
    }
}
