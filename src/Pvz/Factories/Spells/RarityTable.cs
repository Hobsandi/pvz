namespace Application
{
    // Två steg: först slumpas en sällsynthet efter vikt, sedan en
    // spellfabrik inom den sällsyntheten.
    public class RarityTable
    {
        readonly WeightedTable<Rarity> rarities = new();
        readonly Dictionary<Rarity, WeightedTable<ISpellFactory>> spells = [];

        public RarityTable Add(Rarity rarity, ISpellFactory factory, int weight = 1)
        {
            if (!spells.TryGetValue(rarity, out WeightedTable<ISpellFactory>? table))
            {
                table = new();
                spells[rarity] = table;
                rarities.SetWeight(rarity, 1);
            }
            table.Add(factory, weight);
            return this;
        }

        public void SetWeight(Rarity rarity, int weight)
            => rarities.SetWeight(rarity, weight);

        public SpellUpgrade Generate()
        {
            Rarity rarity = rarities.Pick();
            return new SpellUpgrade(spells[rarity].Pick().GenerateSpell(), rarity);
        }
    }
}
