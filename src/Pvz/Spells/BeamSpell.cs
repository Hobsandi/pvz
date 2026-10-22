namespace Application
{
    public class BeamSpell(IReadOnlyList<IEffect> effects, Board board)
        : AreaSpell(effects)
    {
        protected override IEnumerable<Zombie> SelectTargets(
            Position origin)
            => board.FindZombiesInRowOf(origin);

        protected override string DescribeShape() => "Beam";
    }
}
