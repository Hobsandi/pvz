namespace Application
{
    // Observer: KillCounter vet inte vad som händer vid en kill,
    // den meddelar bara alla som prenumererar (uppgraderingar,
    // nya banor, svårighetsgrad).
    public class KillCounter
    {
        readonly List<IKillObserver> observers = [];

        public int Total { get; private set; }

        public void Subscribe(IKillObserver observer) => observers.Add(observer);

        public void Register()
        {
            Total++;
            foreach (IKillObserver observer in observers)
                observer.OnKill(Total);
        }
    }
}
