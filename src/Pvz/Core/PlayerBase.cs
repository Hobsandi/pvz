namespace Application
{
    public class PlayerBase(int health) : IDescribable
    {
        int health = health;

        public void TakeHit() => health--;

        public bool IsDestroyed() => health <= 0;

        public string Describe() => $"Base HP: {health} ";
    }
}
