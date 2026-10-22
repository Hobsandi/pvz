namespace Application
{
    public class PiercingImpactFactory : IImpactFactory
    {
        public IImpact GenerateImpact() => new PiercingImpact();
    }
}
