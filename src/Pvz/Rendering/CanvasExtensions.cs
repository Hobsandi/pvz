namespace Application
{
    public static class CanvasExtensions
    {
        public static void Write(this ICanvas canvas, int x, int y,
            string text, ConsoleColor color = ConsoleColor.Gray)
        {
            for (int i = 0; i < text.Length; i++)
                canvas.Plot(x + i, y, text[i], color);
        }

        public static void WriteCentered(this ICanvas canvas, int y,
            string text, ConsoleColor color = ConsoleColor.Gray)
            => canvas.Write((canvas.Width - text.Length) / 2, y, text, color);

        // Radbryter på mellanslag. Fortsättningsrader flyttas in `indent`
        // steg. Returnerar hur många rader texten tog.
        public static int WriteWrapped(this ICanvas canvas, int x, int y,
            int width, string text, ConsoleColor color = ConsoleColor.Gray,
            int indent = 0)
        {
            List<string> lines = Wrap(text, width, indent);
            for (int i = 0; i < lines.Count; i++)
                canvas.Write(x + (i == 0 ? 0 : indent), y + i, lines[i], color);
            return lines.Count;
        }

        public static List<string> Wrap(string text, int width, int indent = 0)
        {
            List<string> lines = [];
            string line = "";
            foreach (string word in text.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            {
                int room = lines.Count == 0 ? width : width - indent;
                string candidate = line.Length == 0 ? word : $"{line} {word}";
                if (candidate.Length <= room)
                {
                    line = candidate;
                    continue;
                }
                if (line.Length > 0) lines.Add(line);
                line = word.Length <= room ? word : word[..room];
            }
            if (line.Length > 0 || lines.Count == 0) lines.Add(line);
            return lines;
        }

        public static void Fill(this ICanvas canvas, char symbol,
            ConsoleColor color = ConsoleColor.Gray)
        {
            for (int y = 0; y < canvas.Height; y++)
                for (int x = 0; x < canvas.Width; x++)
                    canvas.Plot(x, y, symbol, color);
        }

        public static void DrawBox(this ICanvas canvas, int x, int y,
            int width, int height, ConsoleColor color = ConsoleColor.DarkGray)
        {
            for (int i = 1; i < width - 1; i++)
            {
                canvas.Plot(x + i, y, '─', color);
                canvas.Plot(x + i, y + height - 1, '─', color);
            }
            for (int i = 1; i < height - 1; i++)
            {
                canvas.Plot(x, y + i, '│', color);
                canvas.Plot(x + width - 1, y + i, '│', color);
            }
            canvas.Plot(x, y, '┌', color);
            canvas.Plot(x + width - 1, y, '┐', color);
            canvas.Plot(x, y + height - 1, '└', color);
            canvas.Plot(x + width - 1, y + height - 1, '┘', color);
        }

        // Tömmer en centrerad ruta med ram och returnerar insidan
        // (med en kolumns marginal på varje sida).
        public static Viewport Popup(this ICanvas canvas, int contentWidth,
            int contentHeight, ConsoleColor border = ConsoleColor.White)
        {
            int width = contentWidth + 4;
            int height = contentHeight + 2;
            int x = (canvas.Width - width) / 2;
            int y = (canvas.Height - height) / 2;
            new Viewport(canvas, x, y, width, height).Fill(' ');
            canvas.DrawBox(x, y, width, height, border);
            return new Viewport(canvas, x + 2, y + 1, contentWidth, contentHeight);
        }
    }
}
