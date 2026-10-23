namespace Application
{
    // Ett enda tecken i en färg
    public class SymbolSprite(char symbol, ConsoleColor color) : ISprite
    {
        public void Draw(ICanvas canvas, Position at)
            => at.Plot(canvas, symbol, color);
    }
}
