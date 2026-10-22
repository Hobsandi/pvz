namespace Application
{
    public class AuraSpell(IReadOnlyList<IEffect> effects, Board board)
        : AreaSpell(effects)
    {
        protected override IEnumerable<Zombie> SelectTargets(
            Position origin)
            //board är en ienumerable of zombie
            => board;

        protected override string DescribeShape() => "Aura";
    }
}
