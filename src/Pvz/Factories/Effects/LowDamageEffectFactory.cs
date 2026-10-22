namespace Application
{
    public class LowDamageEffectFactory(int zombieHealth) : IEffectFactory
    {
        public IEffect GenerateEffect()
            => new DamageEffect(Random.Shared.Next(1, zombieHealth / 3 + 1));
    }
}
