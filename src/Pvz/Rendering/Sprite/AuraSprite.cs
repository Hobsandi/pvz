using static Application.GameConfig;

namespace Application
{
    // Fyller hela brädet till höger om origin, på alla rader.
    public class AuraSprite(char symbol, ConsoleColor color) : ISprite
    {
        public void Draw(ICanvas canvas, Position at)
        {
            for (int row = 0; row < ROWS; row++)
            {
                Position pos = at.MoveToRow(row).Shift(PIXEL_SIZE);
                while (!pos.IsPastRightEdge())
                {
                    pos.Plot(canvas, symbol, color);
                    pos = pos.Shift(PIXEL_SIZE);
                }
            }
        }
    }
}
