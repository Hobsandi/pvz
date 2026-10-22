namespace Application
{
    public class ProjectileSpell(
        IReadOnlyList<IEffect> effects,
        Board board,
        int ticksPerStep,
        IImpact impact,
        ISprite sprite) : ISpell
    {
        readonly IEffect effect = new CompositeEffect(effects);

        public void Cast(Position origin)
        {
            Projectile projectile =
                new(origin, effect, impact, sprite, board);
            board.Add(projectile, ticksPerStep);
        }

        public string Describe() => $"Projectile of {effect.Describe()}";
    }
}
