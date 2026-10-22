namespace Application
{
    public class FreezeEffectFactory : IEffectFactory
    {
        public IEffect GenerateEffect() => new FreezeEffect(Random.Shared.Next(2, 6));
    }
}
