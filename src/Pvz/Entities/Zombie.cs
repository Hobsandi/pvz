namespace Application
{
    public class Zombie(
        Position position,
        int health,
        IMovement movement,
        ISprite sprite,
        PlayerBase playerBase,
        KillCounter kills)
        : IUpdater, IDrawable, IComparable<Zombie>
    {
        Position position = position;
        int health = health;
        bool dead = false;
        // How would we implement different status effects?
        int frozenSteps = 0;

        public void Update()
        {
            // Kan ha dött tidigare i samma tick (projektiler uppdateras först)
            if (dead) return;
            if (frozenSteps > 0)
            {
                frozenSteps--;
                return;
            }
            position = movement.Move(position);
            if (!position.IsAtLeftEdge()) return;

            playerBase.TakeHit();
            Die();
        }

        public bool IsAlive() => !dead;

        public void TakeDamage(int amount)
        {
            if (dead) return;
            health -= amount;
            if (health <= 0) Die();
        }

        // Körs en gång, både när zombien dödas och när den når basen
        void Die()
        {
            dead = true;
            kills.Register();
        }

        public void Freeze(int steps)
            => frozenSteps = Math.Max(frozenSteps, steps);

        public bool IsReachedBy(Position other)
            => other.IsAtOrPast(position);

        public bool IsOnSameRowAs(Position other)
            => position.IsOnSameRowAs(other);

        public bool IsWithin(Position center, int radius)
            => position.IsWithin(center, radius);

        public void Draw(ICanvas canvas) => sprite.Draw(canvas, position);

        public int CompareTo(Zombie? other)
            => other == null ? -1 : position.CompareTo(other.position);
    }
}
