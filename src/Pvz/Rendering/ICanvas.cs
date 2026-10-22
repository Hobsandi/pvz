namespace Application
{
    // Både hela skärmen (Canvas) och en del av den (Viewport) är en ICanvas,
    // så allt som ritar behöver inte veta var på skärmen det hamnar.
    public interface ICanvas
    {
        int Width { get; }
        int Height { get; }
        void Plot(int x, int y, char symbol, ConsoleColor color);
    }
}
