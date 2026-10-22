namespace Application
{
    public class Zombie(
        Position position,
        int health,
        IMovement movement,
        ISprite sprite,
        PlayerBase playerBase)
        : IUpdater, IDrawable, IComparable<Zombie>
    {
        Position position = position;
        int health = health;
        // How would we implement different status effects?
        int frozenSteps = 0;

        public void Update()
        {
            if (frozenSteps > 0)
            {
                frozenSteps--;
                return;
            }
            position = movement.Move(position);
            if (!position.IsAtLeftEdge()) return;

            playerBase.TakeHit();
            health = 0;
        }

        public bool IsAlive() => health > 0;

        public void TakeDamage(int amount) => health -= amount;

        public void Freeze(int steps)
            => frozenSteps = Math.Max(frozenSteps, steps);

        public bool IsReachedBy(Position other)
            => other.IsAtOrPast(position);

        public bool IsOnSameRowAs(Position other)
            => position.IsOnSameRowAs(other);

        public bool IsWithin(Position center, int radius)
            => position.IsWithin(center, radius);

        public void Draw(Canvas canvas) => sprite.Draw(canvas, position);

        public int CompareTo(Zombie? other)
            => other == null ? -1 : position.CompareTo(other.position);
    }
}
