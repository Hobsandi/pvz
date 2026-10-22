namespace Application
{
    // En rektangel i en annan canvas. (0,0) här är (x,y) i parent,
    // och allt utanför rektangeln klipps bort.
    public class Viewport(ICanvas parent, int x, int y, int width, int height)
        : ICanvas
    {
        public int Width => width;
        public int Height => height;
        public int OriginX => x;
        public int OriginY => y;

        public void Plot(int px, int py, char symbol, ConsoleColor color)
        {
            if (px < 0 || px >= width || py < 0 || py >= height) return;
            parent.Plot(x + px, y + py, symbol, color);
        }
    }
}
