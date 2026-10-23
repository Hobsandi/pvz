using static Application.GameConfig;

namespace Application
{
    // Spelar en runda. Varje gång Run() anropas byggs en ny bana,
    // så "Start game" igen börjar från noll.
    public class GameRunner : IScreen
    {

        //Game settings
        const int zombieTicks = 6;
        const int spawnTicks = 30;
        const int baseHealth = 3;
        const int startingZombieHealth = 100;
        const int basicSpellDamage = 100;
        const int cannonTicks = 20;


        readonly Canvas canvas = new(COLUMNS, ROWS);
        Board board = new();
        PlayerBase playerBase = new (baseHealth);
        KillCounter killCounter;
        Difficulty difficulty;
        Upgrader upgrader;



        public string Describe()
            => $"Start game";

        public void Run()
        {
            board = new();
            playerBase = new(baseHealth);
            killCounter = new KillCounter();
            SetUp();

            Console.Write("\x1b[2J");
            while (!playerBase.IsDestroyed())
            {
                board.Update();
                // Efter board.Update, så menyn aldrig öppnas mitt i en tick.
                // Spelet står still medan menyn väntar på val.
                while (upgrader.TryTakePending())
                {
                    Display();
                    upgrader.RunUpgrade();
                    // Rensa bort menyn innan planen ritas igen
                    Console.Write("\x1b[2J");
                }
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
            Console.WriteLine($"Zombies killed: {killCounter.Kills}!");

        }

        // Kanoner och zombiespawner, det som låg i Program förut
        void SetUp()
        {
            Random rand = new Random();
            ISpell basicSpell = new ProjectileSpell(
                new DamageEffect(basicSpellDamage),
                board, 3, new SingleImpact(), new SymbolSprite('o')
            );

            difficulty = new Difficulty(startingZombieHealth, playerBase, killCounter, board, rand);
            killCounter.Subscribe(difficulty);

            upgrader = new Upgrader(difficulty, board);
            killCounter.Subscribe(upgrader);

            for (int row = 0; row < 4; row++) {
                // Eget magasin per kanon, annars får alla samma spells
                Cannon cannon = new(new(5, row), new SymbolSprite('C'), new RegularMagazine(2, 5));
                cannon.AddSpell(basicSpell);
                board.Add(cannon, cannonTicks);
            }

            ZombieSpawner spawner = new(board, playerBase, rand, zombieTicks, difficulty);
            board.Register(spawner, spawnTicks);
        }
    }
}
