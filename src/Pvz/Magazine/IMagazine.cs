namespace Application
{
    public interface IMagazine : IEnumerable<ISpell>
    {
        public void Load(ISpell spell);
        public void Cast(Position position);
        public void Upgrade();
    }
}