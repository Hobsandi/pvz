using static Application.GameConfig;

namespace Application
{
    public class LaneUnlocker(Board board, CannonFactory cannons, int killsPerLane)
        : IKillObserver
    {
        public void OnKill(int totalKills)
        {
            if (totalKills % killsPerLane == 0) AddLane();
        }

        public void AddLane()
        {
            if (board.LaneCount >= MAX_LANES) return;

            Cannon cannon = cannons.Create(board.LaneCount);
            board.Add(cannon, CANNON_TICKS);
        }
    }
}
