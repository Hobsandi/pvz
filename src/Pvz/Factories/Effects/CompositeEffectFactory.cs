namespace Application
{
    // Kombinerar flera fabriker till en CompositeEffect, t.ex. skada + frys.
    public class CompositeEffectFactory(IReadOnlyList<IEffectFactory> factories)
        : IEffectFactory
    {
        public IEffect GenerateEffect()
            => new CompositeEffect(factories.Select(f => f.GenerateEffect()).ToList());
    }
}
