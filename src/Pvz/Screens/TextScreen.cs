namespace Application
{
    // Visar en text tills spelaren trycker Enter eller Esc.
    public class TextScreen(string title, string text, IInput input, IScreen back)
        : IScreen
    {
        const int WIDTH = 60;

        public IScreen? Show()
        {
            Console.Write("\x1b[2J");
            string[] paragraphs = text.Split('\n');
            int height = 4 + paragraphs.Sum(p => CanvasExtensions.Wrap(p, WIDTH).Count);

            Canvas canvas = Canvas.FromWindow();
            canvas.Clear();
            ICanvas popup = canvas.Popup(WIDTH, height);
            popup.WriteCentered(0, title, ConsoleColor.White);
            int y = 2;
            foreach (string paragraph in paragraphs)
                y += popup.WriteWrapped(0, y, WIDTH, paragraph);
            popup.WriteCentered(height - 1, "Enter: back", ConsoleColor.DarkGray);
            canvas.Render();

            input.Flush();
            Command command;
            do command = input.Read();
            while (command != Command.Confirm && command != Command.Back);
            return back;
        }

        public string Describe() => title;
    }
}
