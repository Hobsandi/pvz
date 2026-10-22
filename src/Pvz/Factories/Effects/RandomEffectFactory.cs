namespace Application
{
    // Väljer slumpmässigt en av de inskickade fabrikerna varje gång.
    public class RandomEffectFactory(IReadOnlyList<IEffectFactory> factories)
        : IEffectFactory
    {
        public IEffect GenerateEffect()
            => factories[Random.Shared.Next(factories.Count)].GenerateEffect();
    }
}
