using static Application.GameConfig;

namespace Application
{
    // Bygger en ny runda varje gång, så "spela igen" börjar från noll.
    public class GameScreen(IInput input, HighscoreStore highscores,
        IScreen mainMenu) : IScreen
    {
        public IScreen? Show()
        {
            Layout layout = WaitForLargeEnoughWindow();

            Board board = new();
            PlayerBase playerBase = new(BASE_HEALTH);
            KillCounter kills = new();

            LaneUnlocker lanes = new(board,
                new CannonFactory(new BasicSpellFactory(board)), KILLS_PER_LANE);
            for (int lane = 0; lane < START_LANES; lane++)
                lanes.AddLane();

            IZombieFactory walker = new WalkerZombieFactory(playerBase, kills);
            IZombieFactory michael = new MichaelZombieFactory(playerBase, kills);
            IZombieFactory teleporter = new TeleporterZombieFactory(board, playerBase, kills);

            SpawnSettings spawn = new();
            RarityTable spells = SpellCatalog.Create(board);
            RunDirector director = new(
                Stages.Create(walker, michael, teleporter), spawn, spells);
            UpgradeTrigger upgrades = new(FIRST_UPGRADE_AT, UPGRADE_GROWTH);

            kills.Subscribe(upgrades);
            kills.Subscribe(lanes);
            kills.Subscribe(director);

            board.Register(new ZombieSpawner(board, spawn), 1);

            new Game(
                board,
                playerBase,
                upgrades,
                new UpgradeOffer(spells, board, playerBase),
                new CannonInspector(board),
                new Hud(playerBase, board, kills, upgrades, highscores.Best()),
                layout,
                input).Run(TICK_MS);

            return new GameOverScreen(kills.Total, highscores, input, mainMenu);
        }

        static Layout WaitForLargeEnoughWindow()
        {
            while (true)
            {
                Canvas canvas = Canvas.FromWindow();
                if (Layout.Fits(canvas)) return new Layout(canvas);

                canvas.Clear();
                canvas.WriteWrapped(0, 0, canvas.Width,
                    $"Make the window larger: at least {Layout.RequiredWidth}x"
                    + $"{Layout.RequiredHeight}, now {canvas.Width}x{canvas.Height}.",
                    ConsoleColor.Yellow);
                canvas.Render();
                Thread.Sleep(250);
            }
        }

        public string Describe() => "Start game";
    }
}
