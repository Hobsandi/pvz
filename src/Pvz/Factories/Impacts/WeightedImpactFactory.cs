namespace Application
{
    // Skapar en slumpad träff för projektiler: enkel, genomborrande eller
    // explosiv. Varje sort har en vikt som avgör hur ofta den kommer, och
    // explosiv är ovanligast.
    public class WeightedImpactFactory : IImpactFactory
    {
        const int SINGLE_WEIGHT = 6;
        const int PIERCING_WEIGHT = 2;
        const int EXPLOSIVE_WEIGHT = 1;

        readonly WeightedTable<IImpactFactory> impacts;

        public WeightedImpactFactory(Board board, Random rand)
        {
            impacts = new WeightedTable<IImpactFactory>(rand);
            impacts.Add(new SingleImpactFactory(), SINGLE_WEIGHT);
            impacts.Add(new PiercingImpactFactory(), PIERCING_WEIGHT);
            impacts.Add(new ExplosiveImpactFactory(board), EXPLOSIVE_WEIGHT);
        }

        public IImpact GenerateImpact() => impacts.Pick().GenerateImpact();
    }
}
