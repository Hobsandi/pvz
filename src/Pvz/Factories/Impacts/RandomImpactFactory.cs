namespace Application
{
    // Väljer en av fabrikerna efter vikt varje gång.
    public class RandomImpactFactory(WeightedTable<IImpactFactory> factories)
        : IImpactFactory
    {
        public IImpact GenerateImpact() => factories.Pick().GenerateImpact();
    }
}
