namespace Application
{
    // Command: varje val i uppgraderingsmenyn vet själv vad det gör.
    // Den får en kontext i stället för en kanon, eftersom alla
    // uppgraderingar inte behöver en kanon (t.ex. heal).
    public interface IUpgrade : IDescribable
    {
        ConsoleColor Color { get; }
        void Apply(IUpgradeContext context);
    }
}
