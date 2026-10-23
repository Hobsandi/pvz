namespace Application
{
    public class BeamSpell(IEffect effect, Board board)
        : AreaSpell(effect, board)
    {
        protected override ISprite Sprite => new BeamSprite('=', ConsoleColor.Cyan);

        protected override int Ticks => 3;


        protected override IEnumerable<Zombie> SelectTargets(
            Position origin)
            => Board.FindZombiesInRowOf(origin);

        protected override string DescribeShape() => "Beam";
    }
}
