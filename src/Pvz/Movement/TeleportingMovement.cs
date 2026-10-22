using static Application.GameConfig;

namespace Application
{
    public class TeleportingMovement(Random random, int stepsPerTeleport,
        Board board) : IMovement
    {
        int steps = 0;

        public Position Move(Position from)
        {
            steps++;
            Position next = from.Shift(-PIXEL_SIZE);
            if (steps % stepsPerTeleport != 0) return next;

            return next.MoveToRow(random.Next(board.LaneCount));
        }
    }
}
