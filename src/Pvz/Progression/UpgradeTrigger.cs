namespace Application
{
    // Ger en uppgradering vid växande antal kills, t.ex. 5, 13, 25, 43...
    // Menyn öppnas inte här (vi är mitt i board.Update), utan Game
    // hämtar väntande uppgraderingar efter ticken.
    public class UpgradeTrigger(int first, double growth) : IKillObserver
    {
        int step = first;
        int pending = 0;

        public int NextThreshold { get; private set; } = first;

        public void OnKill(int totalKills)
        {
            while (totalKills >= NextThreshold)
            {
                pending++;
                step = (int)Math.Ceiling(step * growth);
                NextThreshold += step;
            }
        }

        public bool TryTakePending()
        {
            if (pending == 0) return false;
            pending--;
            return true;
        }
    }
}
