namespace Application
{
    public class PlayerBase(int maxHealth) : IDescribable
    {
        public int MaxHealth { get; } = maxHealth;
        public int Health { get; private set; } = maxHealth;

        public void TakeHit() => Health--;

        public void Heal(int amount)
            => Health = Math.Min(MaxHealth, Health + amount);

        public bool IsDamaged() => Health < MaxHealth;

        public bool IsDestroyed() => Health <= 0;

        public string Describe() => $"HP {Health}/{MaxHealth}";
    }
}
