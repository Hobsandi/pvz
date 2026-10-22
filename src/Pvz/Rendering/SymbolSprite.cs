namespace Application
{
    //Explain different sprites
    public class SymbolSprite(char symbol) : ISprite
    {
        public void Draw(Canvas canvas, Position at)
            => at.Plot(canvas, symbol);
    }
}
