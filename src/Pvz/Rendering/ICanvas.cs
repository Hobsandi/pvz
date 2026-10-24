namespace Application
{
    public interface ICanvas
    {
        void Plot(int x, int y, char symbol, ConsoleColor color);
        void Plot(int x, int y, char symbol)
        {
            Plot(x,y,symbol,ConsoleColor.Gray);
        }

        void Write(int x, int y, string text)
        {
            string[] lines = text.Split('\n');
            for (int row = 0; row < lines.Length; row++)
                for (int i = 0; i < lines[row].Length; i++)
                    Plot(x + i, y + row, lines[row][i]);
        }

    }
}