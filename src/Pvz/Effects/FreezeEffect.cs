namespace Application
{
    public class FreezeEffect(int steps) : IEffect
    {
        public void Apply(Zombie zombie) => zombie.Freeze(steps);

        public string Describe() => $"freeze for {steps} steps";
    }
}
