namespace Application
{
    // Alla spells som kan erbjudas som uppgradering, sorterade efter
    // sällsynthet. Hur ofta varje sällsynthet dyker upp styrs av Stages.
    public static class SpellCatalog
    {
        public static RarityTable Create(Board board)
        {
            IEffectFactory low = new LowDamageEffectFactory();
            IEffectFactory high = new HighDamageEffectFactory();
            IEffectFactory freeze = new FreezeEffectFactory();
            IEffectFactory poison = new PoisonEffectFactory(board);

            IImpactFactory single = new SingleImpactFactory();
            IImpactFactory piercing = new PiercingImpactFactory();
            IImpactFactory explosive = new ExplosiveImpactFactory(board);

            return new RarityTable()
                .Add(Rarity.Common, new ProjectileSpellFactory(board, low, single,
                    new TrailSprite('>', '-', ConsoleColor.Yellow)))
                .Add(Rarity.Common, new ProjectileSpellFactory(board, freeze, single,
                    new SymbolSprite('*', ConsoleColor.Cyan)))
                .Add(Rarity.Common, new BeamSpellFactory(board, low))

                .Add(Rarity.Rare, new ProjectileSpellFactory(board, low, piercing,
                    new TrailSprite('>', '~', ConsoleColor.Blue)))
                .Add(Rarity.Rare, new ProjectileSpellFactory(board,
                    new CompositeEffectFactory([low, poison]), explosive,
                    new SymbolSprite('@', ConsoleColor.Green)))
                .Add(Rarity.Rare, new ProjectileSpellFactory(board, high, single,
                    new SymbolSprite('O', ConsoleColor.Red)))
                .Add(Rarity.Rare, new BeamSpellFactory(board,
                    new CompositeEffectFactory([low, freeze])))

                .Add(Rarity.Epic, new AuraSpellFactory(board, poison))
                .Add(Rarity.Epic, new AuraSpellFactory(board,
                    new CompositeEffectFactory([low, freeze])))
                .Add(Rarity.Epic, new ProjectileSpellFactory(board, high, explosive,
                    new SymbolSprite('o', ConsoleColor.Red)))
                .Add(Rarity.Epic, new BeamSpellFactory(board,
                    new CompositeEffectFactory([high, freeze])));
        }
    }
}
