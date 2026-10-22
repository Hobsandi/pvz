using static Application.GameConfig;

namespace Application
{
    public readonly struct Position(int x, int row)
        : IComparable<Position>
    {
        readonly int x = x;
        readonly int row = row;

        public Position Shift(int dx) => new(x + dx, row);

        public Position MoveToRow(int newRow) => new(x, newRow);

        public bool IsOnSameRowAs(Position other) => row == other.row;

        public bool IsAtOrPast(Position other)
            => IsOnSameRowAs(other) && x >= other.x;

        public bool IsWithin(Position other, int radius)
            => Math.Abs(x - other.x) <= radius * PIXEL_SIZE
                && Math.Abs(row - other.row) <= radius;

        public bool IsAtLeftEdge() => x <= 0;

        public bool IsPastRightEdge() => x > RIGHT_EDGE;

        // Unsure about this one
        public void Plot(Canvas canvas, char symbol)
            => canvas.Plot(x / PIXEL_SIZE, row, symbol);

        public int CompareTo(Position other) => x.CompareTo(other.x);
    }
}
