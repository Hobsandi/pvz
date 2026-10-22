namespace Application
{
    public class MainMenuScreen : IScreen
    {
        const int WIDTH = 30;

        readonly Menu<IScreen> menu;
        readonly IInput input;

        public MainMenuScreen(IInput input, HighscoreStore highscores)
        {
            this.input = input;
            menu = new("P V Z",
            [
                new GameScreen(input, highscores, this),
                new TextScreen("Information", Texts.Information, input, this),
                new TextScreen("Lore", Texts.Lore, input, this),
                new HighscoreScreen(highscores, input, this),
                new QuitScreen(),
            ]);
        }

        public IScreen? Show()
        {
            Console.Write("\x1b[2J");
            return MenuRunner.Choose(menu, input, () =>
            {
                Canvas canvas = Canvas.FromWindow();
                canvas.Clear();
                menu.Draw(canvas.Popup(WIDTH, menu.Height(WIDTH)));
                canvas.Render();
            });
        }

        public string Describe() => "Main menu";
    }
}
