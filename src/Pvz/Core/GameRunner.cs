using static Application.GameConfig;

namespace Application
{
    // Spelar en runda. Varje gång Run() anropas byggs en ny bana,
    // så "Start game" igen börjar från noll.
    public class GameRunner : IScreen
    {
        readonly Canvas canvas = new(COLUMNS, ROWS);
        Board board = new();
        PlayerBase playerBase = new(BASE_HEALTH);

        public string Describe()
            => $"Start game";

        public void Run()
        {
            board = new();
            playerBase = new(BASE_HEALTH);
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
            ISpell basicSpell = new ProjectileSpell(
                new DamageEffect(BASIC_SPELL_DAMAGE),
                board, 3, new SingleImpact(), new SymbolSprite('o')
            );

            RegularMagazine magazine = new RegularMagazine(2, 5);

            for (int row = 0; row < 4; row++) {
                Cannon cannon = new(new(5, row), new SymbolSprite('C'), magazine);
                cannon.AddSpell(basicSpell);
                board.Add(cannon, CANNON_TICKS);
            }

            ZombieSpawner spawner = new(board, playerBase, new Random(), ZOMBIE_HEALTH, ZOMBIE_TICKS);
            board.Register(spawner, SPAWN_TICKS);
        }
    }
}
