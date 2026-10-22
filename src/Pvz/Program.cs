using static Application.GameConfig;

namespace Application
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new();
            PlayerBase playerBase = new(BASE_HEALTH);

            IEffectFactory anyEffect = new RandomEffectFactory([
                new LowDamageEffectFactory(),
                new HighDamageEffectFactory(),
                new FreezeEffectFactory(),
                new PoisonEffectFactory(board),
            ]);
            IImpactFactory anyImpact = new RandomImpactFactory([
                new SingleImpactFactory(),
                new PiercingImpactFactory(),
                new ExplosiveImpactFactory(board),
            ]);

            // En fabrik per rad, varje kanon får två spells från sin fabrik.
            ISpellFactory[] rowFactories =
            [
                new ProjectileSpellFactory(board, anyEffect, anyImpact,
                    new TrailSprite('>', '-')),
                new BeamSpellFactory(board, new CompositeEffectFactory([
                    new LowDamageEffectFactory(),
                    new FreezeEffectFactory(),
                ])),
                new ProjectileSpellFactory(board,
                    new CompositeEffectFactory([
                        new LowDamageEffectFactory(),
                        new PoisonEffectFactory(board),
                    ]),
                    new ExplosiveImpactFactory(board), new SymbolSprite('@')),
                new RandomSpellFactory([
                    new AuraSpellFactory(board, new PoisonEffectFactory(board)),
                    new BeamSpellFactory(board, anyEffect),
                ]),
            ];

            for (int row = 0; row < ROWS; row++)
            {
                Position position = new(0, row);
                Cannon cannon = new(position, new SymbolSprite('C'));
                cannon.AddSpell(rowFactories[row].GenerateSpell());
                cannon.AddSpell(rowFactories[row].GenerateSpell());
                board.Add(cannon, CANNON_TICKS + row);
            }

            ZombieSpawner spawner = new(board, playerBase, new Random());
            board.Register(spawner, SPAWN_TICKS);
            new Game(board, playerBase, new Canvas(COLUMNS, ROWS)).Run(TICK_MS);
        }
    }
}
