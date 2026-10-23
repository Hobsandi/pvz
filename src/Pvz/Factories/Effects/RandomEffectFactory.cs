namespace Application
{
    // Väljer en av fabrikerna efter vikt varje gång.
    public class RandomEffectFactory(WeightedTable<IEffectFactory> factories)
        : IEffectFactory
    {
        public IEffect GenerateEffect() => factories.Pick().GenerateEffect();
    }
}
