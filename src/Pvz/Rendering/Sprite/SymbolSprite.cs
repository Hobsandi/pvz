namespace Application
{
    //Explain different sprites
    public class SymbolSprite(char symbol, ConsoleColor color = ConsoleColor.Gray)
        : ISprite
    {
        public void Draw(ICanvas canvas, Position at)
            => at.Plot(canvas, symbol, color);
    }
}
