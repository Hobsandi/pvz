using static Application.GameConfig;

namespace Application
{
    // Räknar ut var spelplanen och panelerna hamnar, centrerat i fönstret.
    //
    //              Top (poäng)
    //  ┌ Left ┐ ┌ Gutter+Field ┐ ┌ Right ┐
    //  └──────┘ └──────────────┘ └───────┘
    //              Help
    public class Layout
    {
        const int LEFT_WIDTH = 14;
        const int RIGHT_WIDTH = 36;
        const int BOX_HEIGHT = 14;
        const int GAP = 1;
        // Ram (2) + en gutter-kolumn för '[' framför kanonen
        const int FIELD_BOX_WIDTH = COLUMNS + 3;

        public const int RequiredWidth =
            LEFT_WIDTH + GAP + FIELD_BOX_WIDTH + GAP + RIGHT_WIDTH;
        public const int RequiredHeight = BOX_HEIGHT + 2;

        readonly int leftX, fieldX, rightX, boxY;

        public Canvas Canvas { get; }
        public Viewport Top { get; }
        public Viewport Left { get; }
        // Gutter täcker kolumnen före planen och planens två första,
        // så att "[C]" kan ritas runt vald kanon.
        public Viewport Gutter { get; }
        public Viewport Field { get; }
        public Viewport Right { get; }
        public Viewport Help { get; }

        public static bool Fits(ICanvas canvas)
            => canvas.Width >= RequiredWidth && canvas.Height >= RequiredHeight;

        public Layout(Canvas canvas)
        {
            Canvas = canvas;
            int x = (canvas.Width - RequiredWidth) / 2;
            int y = (canvas.Height - RequiredHeight) / 2;
            leftX = x;
            fieldX = leftX + LEFT_WIDTH + GAP;
            rightX = fieldX + FIELD_BOX_WIDTH + GAP;
            boxY = y + 1;

            int inner = BOX_HEIGHT - 2;
            int laneY = boxY + 1 + (inner - MAX_LANES) / 2;

            Top = new(canvas, x, y, RequiredWidth, 1);
            Left = new(canvas, leftX + 2, boxY + 1, LEFT_WIDTH - 4, inner);
            Gutter = new(canvas, fieldX + 1, laneY, 3, MAX_LANES);
            Field = new(canvas, fieldX + 2, laneY, COLUMNS, MAX_LANES);
            Right = new(canvas, rightX + 2, boxY + 1, RIGHT_WIDTH - 4, inner);
            Help = new(canvas, x, boxY + BOX_HEIGHT, RequiredWidth, 1);
        }

        public void DrawFrames()
        {
            Canvas.DrawBox(leftX, boxY, LEFT_WIDTH, BOX_HEIGHT);
            Canvas.DrawBox(fieldX, boxY, FIELD_BOX_WIDTH, BOX_HEIGHT);
            Canvas.DrawBox(rightX, boxY, RIGHT_WIDTH, BOX_HEIGHT);
        }
    }
}
