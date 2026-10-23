namespace Application
{
    public interface ICanvas
    {
        void Plot(int x, int y, char symbol);

         void Write(int x, int y, string text)
        {
            for (int i = 0; i < text.Length; i++)
                Plot(x + i, y, text[i]);
        }
    }
}