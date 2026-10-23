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
            for (int i = 0; i < text.Length; i++)
                Plot(x + i, y, text[i]);
        }
    }
}