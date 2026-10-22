namespace Application
{
    public class ProjectileSpellFactory(
        Board board,
        IEffectFactory effectFactory,
        IImpactFactory impactFactory,
        ISprite sprite) : ISpellFactory
    {
        public ISpell GenerateSpell()
            => new ProjectileSpell(
                effectFactory.GenerateEffect(),
                board,
                Random.Shared.Next(1, 4),
                impactFactory.GenerateImpact(),
                sprite);
    }
}
