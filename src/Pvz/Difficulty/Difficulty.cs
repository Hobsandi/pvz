namespace Application
{
    // Lyssnar på kills och gör spelet svårare: zombierna får mer hälsa
    // och svårare sorter blir vanligare. Ingen läser värdena utifrån,
    // spawnern ber Difficulty att skapa nästa zombie och uppgraderingar
    // ber den om nästa spell.
    public class Difficulty : IKillObserver
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

        private readonly int startingZombieHealth;
        private readonly WeightedTable<IZombieFactory> zombieFactories;
        private readonly WeightedTable<ISpellFactory> spellFactories;
        private readonly IZombieFactory regular;
        private readonly IZombieFactory fast;
        private readonly IZombieFactory tank;
        private readonly IZombieFactory michael;
        private double multiplier = 1.0;

        public Difficulty(int startingZombieHealth, PlayerBase playerBase,
            KillCounter killCounter, Board board, Random rand)
        {
            this.startingZombieHealth = startingZombieHealth;
            regular = new RegularZombieFactory(playerBase, killCounter);
            fast = new FastZombieFactory(playerBase, killCounter);
            tank = new TankZombieFactory(playerBase, killCounter);
            michael = new MichaelZombieFactory(playerBase, killCounter);

            // I början kommer bara vanliga zombies
            zombieFactories = new WeightedTable<IZombieFactory>(rand);
            zombieFactories.Add(regular, 1);
            zombieFactories.Add(fast, 0);
            zombieFactories.Add(tank, 0);
            zombieFactories.Add(michael, 0);

            // Varje spell får en slumpad effekt efter vikt. Hög skada är ovanligast.
            WeightedTable<IEffectFactory> effects = new WeightedTable<IEffectFactory>(rand);
            effects.Add(new LowDamageEffectFactory(startingZombieHealth), LOW_DAMAGE_WEIGHT);
            effects.Add(new FreezeEffectFactory(), FREEZE_WEIGHT);
            effects.Add(new PoisonEffectFactory(board), POISON_WEIGHT);
            effects.Add(new HighDamageEffectFactory(startingZombieHealth), HIGH_DAMAGE_WEIGHT);
            IEffectFactory anyEffect = new RandomEffectFactory(effects);

            // Projektiler får dessutom en slumpad träff. Explosiv är ovanligast.
            WeightedTable<IImpactFactory> impacts = new WeightedTable<IImpactFactory>(rand);
            impacts.Add(new SingleImpactFactory(), SINGLE_WEIGHT);
            impacts.Add(new PiercingImpactFactory(), PIERCING_WEIGHT);
            impacts.Add(new ExplosiveImpactFactory(board), EXPLOSIVE_WEIGHT);
            IImpactFactory anyImpact = new RandomImpactFactory(impacts);

            // Formen slumpas efter vikt
            spellFactories = new WeightedTable<ISpellFactory>(rand);
            spellFactories.Add(new ProjectileSpellFactory(board, anyEffect, anyImpact,
                new TrailSprite('>', '-')), PROJECTILE_WEIGHT);
            spellFactories.Add(new BeamSpellFactory(board, anyEffect), BEAM_WEIGHT);
            spellFactories.Add(new AuraSpellFactory(board, anyEffect), AURA_WEIGHT);
        }

        public ISpell GenerateSpell()
        {
            return spellFactories.Pick().GenerateSpell();
        }

        public void OnKill(int kills)
        {
            switch (kills) {
                case 10:
                    multiplier = 1.1;
                    zombieFactories.SetWeight(fast, 1);
                    zombieFactories.SetWeight(michael, 1);
                    break;
                case 15:
                    zombieFactories.AddWeight(michael,1020);
                    break;
                case 20:
                    multiplier = 1.2;
                    zombieFactories.SetWeight(tank, 1);
                    break;
                case 30:
                    multiplier = 1.3;
                    zombieFactories.SetWeight(fast, 2);
                    zombieFactories.SetWeight(tank, 2);
                    break;
                // Efter 30: +0.1 var tionde kill (40, 50, 60...)
                case > 30 when kills % 10 == 0:
                    multiplier += 0.1;
                    zombieFactories.AddWeight(michael, 1100);
                    break;
            }
        }

        public Zombie GenerateZombie(Position position)
            => zombieFactories.Pick().GenerateZombie(position,
                (int)(startingZombieHealth * multiplier));
    }
}
