namespace Application
{
    public class AuraSpell(IEffect effect, Board board)
        : AreaSpell(effect, board)
    {
        protected override ISprite Sprite => new AuraSprite('Ö', Board, ConsoleColor.Magenta);

        protected override int Ticks => 3;


        protected override IEnumerable<Zombie> SelectTargets(
            Position origin)
            //board är en ienumerable of zombie
            => Board;

        protected override string DescribeShape() => "Aura";
    }
}
