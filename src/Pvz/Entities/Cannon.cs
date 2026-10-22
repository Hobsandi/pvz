namespace Application
{
    public class Cannon(Position position, ISprite sprite)
        : IUpdater, IDrawable
    {
        readonly List<ISpell> spells = [];
        int index = 0;

        public void AddSpell(ISpell spell) => spells.Add(spell);

        public void Update()
        {
            if (spells.Count == 0) return;

            spells[index].Cast(position);
            index = (index + 1) % spells.Count;
        }

        public bool IsAlive() => true;

        public void Draw(Canvas canvas) => sprite.Draw(canvas, position);
    }
}
