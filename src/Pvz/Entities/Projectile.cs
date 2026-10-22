using static Application.GameConfig;

namespace Application
{
    public class Projectile(
        Position position,
        IEffect effect,
        IImpact impact,
        ISprite sprite,
        Board board) : IUpdater, IDrawable
    {
        Position position = position;
        readonly HashSet<Zombie> struck = [];
        bool alive = true;

        public void Update()
        {
            position = position.Shift(PIXEL_SIZE);
            foreach (Zombie target in board.FindZombiesReachedBy(position))
            {
                if (!struck.Add(target)) continue;

                alive = impact.Strike(target, position, effect);
                if (!alive) return;
            }
            if (position.IsPastRightEdge()) alive = false;
        }

        public bool IsAlive() => alive;

        public void Draw(Canvas canvas) => sprite.Draw(canvas, position);
    }
}
