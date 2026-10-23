using static Application.GameConfig;

namespace Application
{
    public class TrailSprite(char head, char tail) : ISprite
    {
        public void Draw(ICanvas canvas, Position at)
        {
            at.Shift(-PIXEL_SIZE).Plot(canvas, tail);
            at.Plot(canvas, head);
        }
    }
}
