namespace Application
{
    public class ExplosiveImpact(Board board, int radius) : IImpact
    {
        const int FLASH_TICKS = 3;

        public bool Strike(Zombie target, Position at, IEffect effect)
        {
            board.Add(new Flash(new ExplosionSprite('*', radius), at, FLASH_TICKS), 1);
            foreach (Zombie zombie in board.FindZombiesNear(at, radius))
                effect.Apply(zombie);
            return false;
        }
    }
}
