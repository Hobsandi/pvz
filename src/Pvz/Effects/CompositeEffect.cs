namespace Application
{
    public class CompositeEffect(IReadOnlyList<IEffect> effects) : IEffect
    {
        public void Apply(Zombie zombie)
        {
            foreach (IEffect effect in effects)
                effect.Apply(zombie);
        }

        public string Describe()
            => string.Join(" + ", effects.Select(e => e.Describe()));
    }
}
