namespace Application
{
    public class GameOverScreen(int kills, HighscoreStore highscores,
        IInput input, IScreen mainMenu) : IScreen
    {
        const int WIDTH = 44;

        public IScreen? Show()
        {
            bool qualifies = highscores.Qualifies(kills);

            Canvas canvas = Canvas.FromWindow();
            canvas.Clear();
            Viewport popup = canvas.Popup(WIDTH, 7);
            popup.WriteCentered(0, "GAME OVER", ConsoleColor.Red);
            popup.WriteCentered(2, $"Zombies killed: {kills}", ConsoleColor.White);
            popup.WriteCentered(4, qualifies
                ? "Top 10! Enter your name (empty = skip):"
                : "Press Enter to continue", ConsoleColor.Gray);
            canvas.Render();

            input.Flush();
            if (!qualifies)
            {
                while (input.Read() != Command.Confirm) { }
                return mainMenu;
            }

            Console.SetCursorPosition(popup.OriginX, popup.OriginY + 6);
            Console.Write("> ");
            Console.CursorVisible = true;
            string name = Console.ReadLine() ?? "";
            Console.CursorVisible = false;

            // Inget namn: poängen sparas inte
            if (name.Trim().Length == 0) return mainMenu;

            highscores.Add(name, kills);
            return new HighscoreScreen(highscores, input, mainMenu);
        }

        public string Describe() => "Game over";
    }
}
