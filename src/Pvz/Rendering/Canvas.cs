using System.Text;

namespace Application
{
    public class Canvas(int width, int height) : ICanvas
    {
        readonly char[,] cells = new char[height, width];

        public void Clear()
        {
            for (int row = 0; row < height; row++)
                for (int x = 0; x < width; x++)
                    cells[row, x] = ' ';
        }

        // Needs explaining
        public void Plot(int x, int row, char symbol)
        {
            if (x < 0 || x >= width || row < 0 || row >= height) return;
            cells[row, x] = symbol;
        }

        public void Render()
        {
            StringBuilder output = new("\x1b[H");
            for (int row = 0; row < height; row++)
            {
                for (int x = 0; x < width; x++)
                    output.Append(cells[row, x]);
                output.AppendLine();
            }
            Console.Write(output);
        }
    }
}
