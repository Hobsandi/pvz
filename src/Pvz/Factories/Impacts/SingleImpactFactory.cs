namespace Application
{
    public class SingleImpactFactory : IImpactFactory
    {
        public IImpact GenerateImpact() => new SingleImpact();
    }
}
