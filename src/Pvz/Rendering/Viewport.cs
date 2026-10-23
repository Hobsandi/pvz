namespace Application
{
    // Mini-canvas inuti en annan canvas. Offsets visar var och width och height hur stor. Plottar på parentcanvas med offset
    class Viewport(ICanvas parent, int offsetX, int offsetY, int width, int height) : ICanvas
    {
        public void Plot(int x, int y, char symbol)
        {
            if (x < 0 || x >= width || y < 0 || y >= height) return;   // clip to its own area
            parent.Plot(offsetX + x, offsetY + y, symbol);
        }
    }
}