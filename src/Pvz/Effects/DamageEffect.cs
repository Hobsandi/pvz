namespace Application
{
    public class DamageEffect(int amount) : IEffect
    {
        public void Apply(Zombie zombie) => zombie.TakeDamage(amount);

        public string Describe() => $"{amount} damage";
    }
}
