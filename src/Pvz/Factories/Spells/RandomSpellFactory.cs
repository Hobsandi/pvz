namespace Application
{
    public class RandomSpellFactory(IReadOnlyList<ISpellFactory> factories)
        : ISpellFactory
    {
        public ISpell GenerateSpell()
            => factories[Random.Shared.Next(factories.Count)].GenerateSpell();
    }
}
