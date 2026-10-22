namespace Application
{
    public class SpellUpgrade(ISpell spell, Rarity rarity) : IUpgrade
    {
        public ConsoleColor Color => rarity.Color;

        public void Apply(IUpgradeContext context)
            => context.ChooseCannon($"Add \"{spell.Describe()}\" to which cannon?",
                _ => true).Load(spell);

        public string Describe() => $"[{rarity.Name}] {spell.Describe()}";
    }
}
