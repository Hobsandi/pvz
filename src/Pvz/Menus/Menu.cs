namespace Application
{
    // Generisk meny: fungerar för allt som kan beskriva sig självt
    // (kanoner, uppgraderingar, skärmar). Menyn håller bara vad som är
    // valt och hur det ritas. Hur input läses avgör den som använder den
    // (MenuRunner blockerar, CannonInspector gör det mitt i spelet).
    public class Menu<T>(string title, IReadOnlyList<T> options,
        Func<T, ConsoleColor>? colorOf = null) where T : IDescribable
    {
        int index = 0;

        public string Title => title;
        public T Selected => options[index];

        public void MoveUp() => index = (index - 1 + options.Count) % options.Count;

        public void MoveDown() => index = (index + 1) % options.Count;

        // Antal rader Draw behöver vid en viss bredd
        public int Height(int width)
            => 2 + options.Sum(option
                => CanvasExtensions.Wrap(option.Describe(), width - 2).Count);

        public void Draw(ICanvas canvas)
        {
            canvas.WriteCentered(0, title, ConsoleColor.White);
            int y = 2;
            for (int i = 0; i < options.Count; i++)
            {
                bool selected = i == index;
                ConsoleColor color = colorOf?.Invoke(options[i])
                    ?? (selected ? ConsoleColor.Yellow : ConsoleColor.Gray);
                if (selected) canvas.Plot(0, y, '>', ConsoleColor.Yellow);
                y += canvas.WriteWrapped(2, y, canvas.Width - 2,
                    options[i].Describe(), color);
            }
        }
    }
}
