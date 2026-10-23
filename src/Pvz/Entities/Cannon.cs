namespace Application
{
    public class Cannon(Position position, ISprite sprite, IMagazine magazine)
        : IUpdater, IDrawable, IDescribable
    {
        public void AddSpell(ISpell spell) => magazine.Load(spell);

        public void Update()
        {
            magazine.Cast(position);
        }

        public bool IsAlive() => true;

        public void Draw(ICanvas canvas) => sprite.Draw(canvas, position);

        // Kanonens spells, t.ex. "Projectile of 25 damage, Beam of freeze..."
        public string Describe()
            => string.Join(", ", magazine.Select(spell => spell.Describe()));
    }
}
