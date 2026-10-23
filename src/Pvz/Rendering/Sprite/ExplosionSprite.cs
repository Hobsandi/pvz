using static Application.GameConfig;

namespace Application
{
    // En fyrkant runt träffpunkten, lika stor som ytan ExplosiveImpact
    // skadar (radius kolumner åt sidorna och radius rader upp och ner).
    // Det som hamnar utanför planen klipps bort av Canvas.Plot.
    public class ExplosionSprite(char symbol, int radius, ConsoleColor color) : ISprite
    {
        public void Draw(ICanvas canvas, Position at)
        {
            for (int dRow = -radius; dRow <= radius; dRow++)
                for (int dx = -radius; dx <= radius; dx++)
                    at.ShiftRow(dRow).Shift(dx * PIXEL_SIZE).Plot(canvas, symbol, color);
        }
    }
}
