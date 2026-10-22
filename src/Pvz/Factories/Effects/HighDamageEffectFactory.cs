namespace Application
{
    public class HighDamageEffectFactory : IEffectFactory
    {
        public IEffect GenerateEffect()
            => new DamageEffect(Random.Shared.Next(GameConfig.ZOMBIE_HEALTH / 2, GameConfig.ZOMBIE_HEALTH + 1));
    }
}
