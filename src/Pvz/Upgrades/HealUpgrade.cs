namespace Application
{
    public class HealUpgrade(int amount) : IUpgrade
    {
        public ConsoleColor Color => ConsoleColor.Green;

        public void Apply(IUpgradeContext context)
            => context.PlayerBase.Heal(amount);

        public string Describe() => $"Heal the base +{amount} HP";
    }
}
