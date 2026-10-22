namespace Application
{
    public class HighDamageEffectFactory(int zombieHealth) : IEffectFactory
    {
        public IEffect GenerateEffect()
            => new DamageEffect(Random.Shared.Next(zombieHealth / 2, zombieHealth + 1));
    }
}
