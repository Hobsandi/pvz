namespace Application
{
    // Skapar slumpade spells. Formen (projektil, beam, aura) slumpas efter
    // vikt, och effekten och träffen kommer från fabrikerna som injiceras.
    // Projektiler är vanligast och aura väldigt ovanlig.
    public class WeightedSpellFactory : ISpellFactory
    {
        const int PROJECTILE_WEIGHT = 12;
        const int BEAM_WEIGHT = 4;
        const int AURA_WEIGHT = 1;

        readonly WeightedTable<ISpellFactory> shapes;

        public WeightedSpellFactory(
            Board board,
            IEffectFactory effects,
            IImpactFactory impacts,
            Random rand)
        {
            ISprite sprite = new TrailSprite('>', '-', ConsoleColor.Yellow);

            shapes = new WeightedTable<ISpellFactory>(rand);
            shapes.Add(
                new ProjectileSpellFactory(board, effects, impacts, sprite),
                PROJECTILE_WEIGHT);
            shapes.Add(new BeamSpellFactory(board, effects), BEAM_WEIGHT);
            shapes.Add(new AuraSpellFactory(board, effects), AURA_WEIGHT);
        }

        public ISpell GenerateSpell() => shapes.Pick().GenerateSpell();
    }
}
