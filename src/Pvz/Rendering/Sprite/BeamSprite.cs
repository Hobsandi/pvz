namespace Application
{
    // En linje från origin till högerkanten
    public class BeamSprite(char symbol, ConsoleColor color) : ISprite
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
