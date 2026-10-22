namespace Application
{
    public class SingleImpact : IImpact
    {
        public bool Strike(Zombie target, Position at, IEffect effect)
        {
            effect.Apply(target);
            return false;
        }
    }
}
