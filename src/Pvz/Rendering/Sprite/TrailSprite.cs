using static Application.GameConfig;

namespace Application
{
    // Ett huvud och en svans bakom, t.ex. "->"
    public class TrailSprite(char head, char tail, ConsoleColor color) : ISprite
    {
        public void Draw(ICanvas canvas, Position at)
        {
            at.Shift(-PIXEL_SIZE).Plot(canvas, tail, color);
            at.Plot(canvas, head, color);
        }
    }
}
