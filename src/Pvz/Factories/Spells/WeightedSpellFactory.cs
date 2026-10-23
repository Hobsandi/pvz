namespace Application
{
    // Skapar slumpade spells i tre steg, alla efter vikt:
    // en form (projektil, beam, aura), en effekt och, för projektiler, en träff.
    // Är själv en ISpellFactory, så den som använder den behöver inte veta hur.
    public class WeightedSpellFactory : ISpellFactory
    {
        // Hur vanlig varje form är. Projektiler är vanligast, aura väldigt ovanlig.
        private const int PROJECTILE_WEIGHT = 12;
        private const int BEAM_WEIGHT = 4;
        private const int AURA_WEIGHT = 1;

        // Hur vanlig varje effekt är
        private const int LOW_DAMAGE_WEIGHT = 6;
        private const int FREEZE_WEIGHT = 3;
        private const int POISON_WEIGHT = 2;
        private const int HIGH_DAMAGE_WEIGHT = 1;

        // Hur vanlig varje träff är (bara projektiler)
        private const int SINGLE_WEIGHT = 6;
        private const int PIERCING_WEIGHT = 2;
        private const int EXPLOSIVE_WEIGHT = 1;

        private readonly WeightedTable<ISpellFactory> shapes;

        public WeightedSpellFactory(Board board, int zombieHealth, Random rand)
        {
            IEffectFactory anyEffect = CreateEffects(board, zombieHealth, rand);
            IImpactFactory anyImpact = CreateImpacts(board, rand);
            shapes = CreateShapes(board, anyEffect, anyImpact, rand);
        }

        public ISpell GenerateSpell() => shapes.Pick().GenerateSpell();

        // Hög skada är ovanligast
        private static IEffectFactory CreateEffects(Board board, int zombieHealth, Random rand)
        {
            WeightedTable<IEffectFactory> effects = new WeightedTable<IEffectFactory>(rand);
            effects.Add(new LowDamageEffectFactory(zombieHealth), LOW_DAMAGE_WEIGHT);
            effects.Add(new FreezeEffectFactory(), FREEZE_WEIGHT);
            effects.Add(new PoisonEffectFactory(board), POISON_WEIGHT);
            effects.Add(new HighDamageEffectFactory(zombieHealth), HIGH_DAMAGE_WEIGHT);
            return new RandomEffectFactory(effects);
        }

        // Explosiv är ovanligast
        private static IImpactFactory CreateImpacts(Board board, Random rand)
        {
            WeightedTable<IImpactFactory> impacts = new WeightedTable<IImpactFactory>(rand);
            impacts.Add(new SingleImpactFactory(), SINGLE_WEIGHT);
            impacts.Add(new PiercingImpactFactory(), PIERCING_WEIGHT);
            impacts.Add(new ExplosiveImpactFactory(board), EXPLOSIVE_WEIGHT);
            return new RandomImpactFactory(impacts);
        }

        private static WeightedTable<ISpellFactory> CreateShapes(Board board,
            IEffectFactory anyEffect, IImpactFactory anyImpact, Random rand)
        {
            WeightedTable<ISpellFactory> shapes = new WeightedTable<ISpellFactory>(rand);
            shapes.Add(new ProjectileSpellFactory(board, anyEffect, anyImpact,
                new TrailSprite('>', '-', ConsoleColor.Yellow)), PROJECTILE_WEIGHT);
            shapes.Add(new BeamSpellFactory(board, anyEffect), BEAM_WEIGHT);
            shapes.Add(new AuraSpellFactory(board, anyEffect), AURA_WEIGHT);
            return shapes;
        }
    }
}
