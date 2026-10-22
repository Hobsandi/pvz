using static Application.GameConfig;

namespace Application
{
    // Fyller hela brädet till höger om origin, på alla rader.
    public class AuraSprite(char symbol) : ISprite
    {
        public void Draw(Canvas canvas, Position at)
        {
            for (int row = 0; row < ROWS; row++)
            {
                Position pos = at.MoveToRow(row).Shift(PIXEL_SIZE);
                while (!pos.IsPastRightEdge())
                {
                    pos.Plot(canvas, symbol);
                    pos = pos.Shift(PIXEL_SIZE);
                }
            }
        }
    }
}
