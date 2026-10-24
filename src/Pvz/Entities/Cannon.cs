namespace Application
{
    public class Cannon(Position position, ISprite sprite, IMagazine magazine, int lane)
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
        // Banans nummer överst, sedan en spell per rad
        public string Describe()
        {
            string output = $"Lane {lane + 1}";
            int number = 1;
            foreach (ISpell spell in magazine)
                output += $"\n{number++}. {spell.Describe()}";
            return output;
        }

    }
}
