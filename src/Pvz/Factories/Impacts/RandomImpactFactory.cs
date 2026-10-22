namespace Application
{
    public class RandomImpactFactory(WeightedTable<IImpactFactory> factories)
        : IImpactFactory
    {
        public RandomImpactFactory(IReadOnlyList<IImpactFactory> factories)
            : this(WeightedTable<IImpactFactory>.Uniform(factories)) { }

        public IImpact GenerateImpact() => factories.Pick().GenerateImpact();
    }
}
