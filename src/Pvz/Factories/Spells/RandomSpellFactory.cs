namespace Application
{
    public class RandomSpellFactory(WeightedTable<ISpellFactory> factories)
        : ISpellFactory
    {
        public RandomSpellFactory(IReadOnlyList<ISpellFactory> factories)
            : this(WeightedTable<ISpellFactory>.Uniform(factories)) { }

        public ISpell GenerateSpell() => factories.Pick().GenerateSpell();
    }
}
