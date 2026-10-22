namespace Application
{
    // Enumerationen ger innehållet (för att visa det), Next() ger
    // skjutordningen. Olika magasin kan skjuta i olika ordning.
    public interface IMagazine : IEnumerable<ISpell>, IDescribable
    {
        bool IsFull { get; }
        bool CanGrow { get; }
        void Load(ISpell spell);
        ISpell? Next();
        void IncreaseCapacity();
    }
}
