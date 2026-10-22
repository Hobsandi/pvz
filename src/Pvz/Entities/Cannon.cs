namespace Application
{
    public class Cannon(Position position, int lane, IMagazine magazine,
        ISprite sprite) : IUpdater, IDrawable, IDescribable
    {
        public int Lane => lane;
        public IEnumerable<ISpell> Spells => magazine;
        public bool IsFull => magazine.IsFull;
        public bool CanGrow => magazine.CanGrow;

        public void Load(ISpell spell) => magazine.Load(spell);

        public void IncreaseCapacity() => magazine.IncreaseCapacity();

        public void Update() => magazine.Next()?.Cast(position);

        public bool IsAlive() => true;

        public void Draw(ICanvas canvas) => sprite.Draw(canvas, position);

        public string Describe() => $"Lane {lane + 1}   {magazine.Describe()}";
    }
}
