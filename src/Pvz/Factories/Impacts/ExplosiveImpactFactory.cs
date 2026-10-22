namespace Application
{
    public class ExplosiveImpactFactory(Board board) : IImpactFactory
    {
        public IImpact GenerateImpact()
            => new ExplosiveImpact(board, Random.Shared.Next(1, 3));
    }
}
