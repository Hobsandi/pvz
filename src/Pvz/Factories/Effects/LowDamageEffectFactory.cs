namespace Application
{
    public class LowDamageEffectFactory : IEffectFactory
    {
        public IEffect GenerateEffect()
            => new DamageEffect(Random.Shared.Next(1, GameConfig.ZOMBIE_HEALTH / 3 + 1));
    }
}
