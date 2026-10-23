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

        // Layout
        const int HUD_GAP = 1;      // tomma rader mellan plan och HUD
        const int HUD_HEIGHT = 2;   // HP och kills


        Canvas canvas;
        // ICanvas och inte Viewport: Write är en default-metod i ICanvas,
        // och sådana går bara att anropa via gränssnittstypen
        ICanvas field;
        ICanvas hud;

        Board board = new();
        PlayerBase playerBase = new (baseHealth);
        KillCounter killCounter;
        Difficulty difficulty;
        Upgrader upgrader;



        public string Describe()
            => $"Start game";

        public void Run()
        {
            Console.CursorVisible = false;


            SetUpViewports();


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

        // Planen centrerad i fönstret, med HUD:en direkt under:
        //
        //     C....o.....Z.............   <- field
        //     C.........M..............
        //                                 <- en tom rad
        //     Base HP: 3                  <- hud
        //     Zombies killed: 12!
        //
        // Hela blocket (plan + mellanrum + HUD) centreras.
        void SetUpViewports()
        {
            // -1 så att terminalen inte radbryter eller scrollar på sista raden
            int width = Console.WindowWidth - 1;
            int height = Console.WindowHeight - 1;
            canvas = new Canvas(width, height);

            int blockHeight = ROWS + HUD_GAP + HUD_HEIGHT;
            int x = Math.Max(0, (width - COLUMNS) / 2);
            int y = Math.Max(0, (height - blockHeight) / 2);

            field = new Viewport(canvas, x, y, COLUMNS, ROWS);
            hud = new Viewport(canvas, x, y + ROWS + HUD_GAP, COLUMNS, HUD_HEIGHT);
        }

        // Ritar en bild av spelet: planen och basens hälsa.
        public void Display()
        {
            canvas.Clear();
            DrawFieldBackground();
            board.Draw(field);

            hud.Write(0, 0, playerBase.Describe());
            hud.Write(0, 1, $"Zombies killed: {killCounter.Kills}!");

            canvas.Render();
        }

        // Prickar på planen, så man ser var den är (Canvas.Clear fyller med mellanslag)
        void DrawFieldBackground()
        {
            for (int row = 0; row < ROWS; row++)
                for (int col = 0; col < COLUMNS; col++)
                    field.Plot(col, row, '.');
        }

        // Kanoner och zombiespawner, det som låg i Program förut
        void SetUp()
        {
            Random rand = new Random();
            ISpell basicSpell = new ProjectileSpell(
                new DamageEffect(basicSpellDamage),
                board, 3, new ExplosiveImpact(board,1), new SymbolSprite('o')
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
