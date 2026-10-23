namespace Application
{
    public class Cannon(Position position, ISprite sprite, IMagazine magazine)
        : IUpdater, IDrawable
    {
        int index = 0;

        public void AddSpell(ISpell spell) => magazine.Load(spell);

        public void Update()
        {
            magazine.Cast(position);
        }

        public bool IsAlive() => true;

        public void Draw(Canvas canvas) => sprite.Draw(canvas, position);
    }
}
