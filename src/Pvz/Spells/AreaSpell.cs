namespace Application
{
    // Should we pass a composite effect to the constructor instead?
    public abstract class AreaSpell(IEffect effect, Board board)
        : ISpell
    {

        protected Board Board { get; } = board;

        public void Cast(Position origin)
        {
            Board.Add(new Flash(Sprite,origin,Ticks),1);
            foreach (Zombie zombie in SelectTargets(origin))
                effect.Apply(zombie);            
        }

        public string Describe()
            => $"{DescribeShape()} of {effect.Describe()}";

        protected abstract IEnumerable<Zombie> SelectTargets(
            Position origin);

        protected abstract ISprite Sprite {get; }
        protected abstract int Ticks {get; }
        protected abstract string DescribeShape();
    }
}
