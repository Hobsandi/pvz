using System.Text;

namespace Application
{
    public class Canvas(int width, int height) : ICanvas
    {
        readonly AnsiPalette palette = new();
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
            // Null = ingen färg skriven än, så första cellen
            // får alltid en kod
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
                        output.Append(palette.Encode(cellColors[row, x]));
                    }
                    output.Append(cells[row, x]);
                }
                output.AppendLine();
            }
            // En gång i slutet, så att text efter spelet inte blir färgad
            output.Append(palette.Reset());
            Console.Write(output);
        }
    }
}
