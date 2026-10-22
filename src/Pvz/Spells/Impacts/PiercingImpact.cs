namespace Application
{
    // Goes through all enemies. Could we implement piercing that dies after a few hits?
    public class PiercingImpact : IImpact
    {
        public bool Strike(Zombie target, Position at, IEffect effect)
        {
            effect.Apply(target);
            return true;
        }
    }
}
