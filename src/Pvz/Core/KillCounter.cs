namespace Application
{
    public class KillCounter
    {
        private int _kills = 0;
        private readonly List<IKillObserver> _observers = new();

        public int Kills => _kills;

        public void Subscribe(IKillObserver observer)
        {
            _observers.Add(observer);
        }

        public void Increment()
        {
            _kills++;
            foreach (var observer in _observers)
            {
                observer.OnKill(_kills);
            }
        }
    }
}