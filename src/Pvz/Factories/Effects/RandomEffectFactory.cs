namespace Application
{
    // Väljer en av fabrikerna efter vikt varje gång.
    public class RandomEffectFactory(WeightedTable<IEffectFactory> factories)
        : IEffectFactory
    {
        // Alla lika troliga
        public RandomEffectFactory(IReadOnlyList<IEffectFactory> factories)
            : this(WeightedTable<IEffectFactory>.Uniform(factories)) { }

        public IEffect GenerateEffect() => factories.Pick().GenerateEffect();
    }
}
