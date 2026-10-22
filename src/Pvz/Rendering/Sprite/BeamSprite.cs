namespace Application
{
    public class BeamSprite(char symbol, ConsoleColor color = ConsoleColor.Gray)
        : ISprite
    {
        public void Draw(ICanvas canvas, Position at)
        {
            Position pos = at.Shift(GameConfig.PIXEL_SIZE);
            while (!pos.IsPastRightEdge())
            {
                pos.Plot(canvas, symbol, color);
                pos = pos.Shift(GameConfig.PIXEL_SIZE);
            }
        }
    }
}
