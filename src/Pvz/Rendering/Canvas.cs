using System.Data;
using System.Text;

namespace Application
{
    public class Canvas(int width, int height) : ICanvas
    {
        // ANSI-färgkod för varje ConsoleColor, i samma ordning som enumen:
        // Black, DarkBlue, DarkGreen, DarkCyan, DarkRed, DarkMagenta, DarkYellow, Gray,
        // DarkGray, Blue, Green, Cyan, Red, Magenta, Yellow, White
        readonly int[] AnsiCodes =
            [30, 34, 32, 36, 31, 35, 33, 37, 90, 94, 92, 96, 91, 95, 93, 97];

        // T.ex. ConsoleColor.Red -> "\x1b[91m". Skrivs före ett tecken för att byta färg.
        string ColorCode(ConsoleColor color) => $"\x1b[{AnsiCodes[(int)color]}m";

        // Skrivs sist i Render, så att text efter spelet inte blir färgad
        const string RESET_COLOR = "\x1b[0m";

        readonly char[,] cells = new char[height, width];
        readonly ConsoleColor[,] cellColors = new ConsoleColor[height,width]; 

        public void Clear()
        {
            for (int row = 0; row < height; row++)
                for (int x = 0; x < width; x++)
                {
                    cells[row, x] = ' ';
                    cellColors[row,x] = ConsoleColor.Gray;
                }
        }

        public void Plot(int x, int row, char symbol, ConsoleColor color)
        {
            if (x < 0 || x >= width || row < 0 || row >= height) return;
            cellColors[row,x] = color;
            cells[row, x] = symbol;
        }


        public void Render()
        {
            StringBuilder output = new("\x1b[H");
            // Null = ingen färg skriven än, så första cellen får alltid en kod
            ConsoleColor? currentColor = null;
            for (int row = 0; row < height; row++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Skriv bara en färgkod när färgen byts. En rad med 40
                    // gråa prickar behöver en kod, inte 40.
                    if (cellColors[row, x] != currentColor)
                    {
                        currentColor = cellColors[row, x];
                        output.Append(ColorCode(cellColors[row, x]));
                    }
                    output.Append(cells[row, x]);
                }
                output.AppendLine();
            }
            // En gång i slutet, så att text efter spelet inte blir färgad
            output.Append(RESET_COLOR);
            Console.Write(output);


        }
    }
}
