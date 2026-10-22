using static Application.GameConfig;

namespace Application
{
    public class TrailSprite(char head, char tail,
        ConsoleColor color = ConsoleColor.Gray) : ISprite
    {
        public void Draw(ICanvas canvas, Position at)
        {
            at.Shift(-PIXEL_SIZE).Plot(canvas, tail, color);
            at.Plot(canvas, head, color);
        }
    }
}
