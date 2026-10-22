namespace Application
{
    public class PoisonEffectFactory(IWorld world) : IEffectFactory
    {
        public IEffect GenerateEffect()
            => new PoisonEffect(world, 1, Random.Shared.Next(3, 6), 10);
    }
}
