namespace Application
{
    public class ProjectileSpell(
        IEffect effect,
        Board board,
        int ticksPerStep,
        IImpact impact,
        ISprite sprite) : ISpell
    {

        public void Cast(Position origin)
        {
            Projectile projectile =
                new(origin, effect, impact, sprite, board);
            board.Add(projectile, ticksPerStep);
        }

        public string Describe() => $"Projectile of {effect.Describe()}";
    }
}
