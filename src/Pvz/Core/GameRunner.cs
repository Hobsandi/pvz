using static Application.GameConfig;

namespace Application
{
    // Spelar en runda. Varje gång Run() anropas byggs en ny bana,
    // så "Start game" igen börjar från noll.
    public class GameRunner(Settings settings) : IScreen
    {
        readonly Canvas canvas = new(COLUMNS, ROWS);
        Board board = new();
        PlayerBase playerBase = new(settings.BaseHealth);

        public string Describe()
            => $"Start game";

        public void Run()
        {
            board = new();
            playerBase = new(settings.BaseHealth);
            SetUp();

            Console.Write("\x1b[2J");
            while (!playerBase.IsDestroyed())
            {
                board.Update();
                Display();
                Thread.Sleep(TICK_MS);
            }
            Console.WriteLine("Game over! Press Enter to return to the menu.");
            Console.ReadLine();
        }

        // Ritar en bild av spelet: planen och basens hälsa.
        public void Display()
        {
            canvas.Clear();
            board.Draw(canvas);
            canvas.Render();
            Console.WriteLine(playerBase.Describe());
        }

        // Kanoner och zombiespawner, det som låg i Program förut
        void SetUp()
        {
            IEffectFactory anyEffect = new RandomEffectFactory([
                new LowDamageEffectFactory(settings.ZombieHealth),
                new HighDamageEffectFactory(settings.ZombieHealth),
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
                    new LowDamageEffectFactory(settings.ZombieHealth),
                    new FreezeEffectFactory(),
                ])),


                new ProjectileSpellFactory(board,
                    new CompositeEffectFactory([
                        new LowDamageEffectFactory(settings.ZombieHealth),
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
                board.Add(cannon, settings.CannonTicks + row);
            }

            ZombieSpawner spawner = new(board, playerBase, new Random(),
                settings.ZombieHealth, settings.ZombieTicks);
            board.Register(spawner, settings.SpawnTicks);
        }
    }
}
