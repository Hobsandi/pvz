using static Application.GameConfig;

namespace Application
{
    // Fyller alla aktiva rader till höger om origin.
    public class AuraSprite(char symbol, Board board,
        ConsoleColor color = ConsoleColor.Gray) : ISprite
    {
        public void Draw(ICanvas canvas, Position at)
        {
            for (int row = 0; row < board.LaneCount; row++)
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
