namespace Application
{
    public class ExplosiveImpact(Board board, int radius) : IImpact
    {
        public bool Strike(Zombie target, Position at, IEffect effect)
        {
            foreach (Zombie zombie in board.FindZombiesNear(at, radius))
                effect.Apply(zombie);
            return false;
        }
    }
}
