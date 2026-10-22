namespace Application
{
    public class Game(
        Board board,
        PlayerBase playerBase,
        UpgradeTrigger upgrades,
        UpgradeOffer offer,
        CannonInspector inspector,
        Hud hud,
        Layout layout,
        IInput input) : IUpgradeContext
    {
        const int POPUP_WIDTH = 56;

        public PlayerBase PlayerBase => playerBase;

        public void Run(int tickMs)
        {
            Console.Write("\x1b[2J");
            while (!playerBase.IsDestroyed())
            {
                while (input.TryRead(out Command command))
                    inspector.Handle(command);

                board.Update();

                // Utanför board.Update, så menyn aldrig öppnas mitt i en tick.
                // Spelet står still medan menyn är öppen.
                while (upgrades.TryTakePending())
                    ChooseUpgrade();

                Render();
                Thread.Sleep(tickMs);
            }
        }

        void ChooseUpgrade()
        {
            Menu<IUpgrade> menu = new("Choose an upgrade", offer.Generate(),
                upgrade => upgrade.Color);
            hud.Prompt = "UPGRADE!";
            IUpgrade upgrade = MenuRunner.Choose(menu, input, () =>
            {
                Draw();
                menu.Draw(layout.Canvas.Popup(POPUP_WIDTH, menu.Height(POPUP_WIDTH)));
                layout.Canvas.Render();
            });
            hud.Prompt = null;
            upgrade.Apply(this);
        }

        public Cannon ChooseCannon(string prompt, Predicate<Cannon> canChoose)
        {
            hud.Prompt = prompt;
            Cannon cannon = MenuRunner.Choose(inspector.Menu, input, Render, canChoose);
            hud.Prompt = null;
            return cannon;
        }

        void Draw()
        {
            layout.Canvas.Clear();
            layout.DrawFrames();
            board.Draw(layout.Field);
            inspector.Draw(layout);
            hud.Draw(layout);
        }

        void Render()
        {
            Draw();
            layout.Canvas.Render();
        }
    }
}
