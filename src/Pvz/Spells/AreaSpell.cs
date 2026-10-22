namespace Application
{
    // Should we pass a composite effect to the constructor instead?
    public abstract class AreaSpell(IReadOnlyList<IEffect> effects)
        : ISpell
    {
        readonly IEffect effect = new CompositeEffect(effects);

        public void Cast(Position origin)
        {
            foreach (Zombie zombie in SelectTargets(origin))
                effect.Apply(zombie);
        }

        public string Describe()
            => $"{DescribeShape()} of {effect.Describe()}";

        protected abstract IEnumerable<Zombie> SelectTargets(
            Position origin);

        protected abstract string DescribeShape();
    }
}
