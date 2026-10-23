namespace Application
{
    public class KillCounter
    {
        private int _kills = 0;

        public int Kills => _kills;

        public void Increment()
        {
            _kills++;
        }
    }
}