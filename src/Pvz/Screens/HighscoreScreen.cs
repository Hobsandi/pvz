namespace Application
{
    public class HighscoreScreen(HighscoreStore highscores, IInput input,
        IScreen back) : IScreen
    {
        const int WIDTH = 30;

        public IScreen? Show()
        {
            Console.Write("\x1b[2J");
            IReadOnlyList<Score> scores = highscores.Top();
            int height = 4 + Math.Max(1, scores.Count);

            Canvas canvas = Canvas.FromWindow();
            canvas.Clear();
            ICanvas popup = canvas.Popup(WIDTH, height);
            popup.WriteCentered(0, "Highscores", ConsoleColor.White);
            if (scores.Count == 0)
                popup.WriteCentered(2, "No scores yet", ConsoleColor.DarkGray);
            for (int i = 0; i < scores.Count; i++)
                popup.Write(0, 2 + i,
                    $"{i + 1,2}. {scores[i].Name,-HighscoreStore.MAX_NAME_LENGTH} {scores[i].Kills,6}",
                    i == 0 ? ConsoleColor.Yellow : ConsoleColor.Gray);
            popup.WriteCentered(height - 1, "Enter: back", ConsoleColor.DarkGray);
            canvas.Render();

            input.Flush();
            Command command;
            do command = input.Read();
            while (command != Command.Confirm && command != Command.Back);
            return back;
        }

        public string Describe() => "Highscores";
    }
}
