using System.Text;

namespace Application
{
    public class Canvas(int width, int height) : ICanvas
    {
        // ConsoleColor i ordning -> ANSI-färgkod
        static readonly int[] AnsiCodes =
            [30, 34, 32, 36, 31, 35, 33, 37, 90, 94, 92, 96, 91, 95, 93, 97];

        readonly char[,] cells = new char[height, width];
        readonly ConsoleColor[,] colors = new ConsoleColor[height, width];

        public int Width => width;
        public int Height => height;

        // En kolumn och rad mindre än fönstret, annars radbryter terminalen.
        public static Canvas FromWindow()
            => new(Math.Max(1, Console.WindowWidth - 1),
                Math.Max(1, Console.WindowHeight - 1));

        public void Clear()
        {
            for (int row = 0; row < height; row++)
                for (int x = 0; x < width; x++)
                {
                    cells[row, x] = ' ';
                    colors[row, x] = ConsoleColor.Gray;
                }
        }

        // Allt utanför canvasen ignoreras, så sprites kan ritas
        // delvis utanför kanten utan att krascha.
        public void Plot(int x, int row, char symbol, ConsoleColor color)
        {
            if (x < 0 || x >= width || row < 0 || row >= height) return;
            cells[row, x] = symbol;
            colors[row, x] = color;
        }

        public void Render()
        {
            StringBuilder output = new("\x1b[H");
            ConsoleColor? current = null;
            for (int row = 0; row < height; row++)
            {
                if (row > 0) output.Append('\n');
                for (int x = 0; x < width; x++)
                {
                    // Skriv bara en färgkod när färgen byts
                    if (colors[row, x] != current)
                    {
                        current = colors[row, x];
                        output.Append($"\x1b[{AnsiCodes[(int)current]}m");
                    }
                    output.Append(cells[row, x]);
                }
            }
            output.Append("\x1b[0m");
            Console.Write(output);
        }
    }
}
