namespace Application
{
    public class RandomImpactFactory(IReadOnlyList<IImpactFactory> factories)
        : IImpactFactory
    {
        public IImpact GenerateImpact()
            => factories[Random.Shared.Next(factories.Count)].GenerateImpact();
    }
}
