using System.Collections;

namespace Application
{
    public class Board : IWorld, IEnumerable<Zombie>
    {
        readonly EntityList<Cannon> cannons = new();
        readonly EntityList<Projectile> projectiles = new();
        readonly EntityList<Zombie> zombies = new();
        readonly EntityList<IUpdater> others = new();

        public void Register(IUpdater updater, int ticksPerUpdate)
            => others.Add(updater, ticksPerUpdate);

        public void Add(Cannon cannon, int ticksPerUpdate)
            => cannons.Add(cannon, ticksPerUpdate);

        public void Add(Projectile projectile, int ticksPerUpdate)
            => projectiles.Add(projectile, ticksPerUpdate);

        public void Add(Zombie zombie, int ticksPerUpdate)
            => zombies.Add(zombie, ticksPerUpdate);

        public void Update()
        {
            cannons.Update();
            projectiles.Update();
            zombies.Update();
            others.Update();
            cannons.RemoveDead();
            projectiles.RemoveDead();
            zombies.RemoveDead();
            others.RemoveDead();
        }

        public IEnumerable<Zombie> FindZombiesReachedBy(Position position)
            => zombies
                .Where(zombie => zombie.IsAlive())
                .Where(zombie => zombie.IsReachedBy(position))
                .Order();

        public IEnumerable<Zombie> FindZombiesInRowOf(Position position)
            => zombies.Where(zombie => zombie.IsOnSameRowAs(position));

        public IEnumerable<Zombie> FindZombiesNear(
            Position center, int radius)
            => zombies.Where(zombie => zombie.IsWithin(center, radius));

        public void Draw(Canvas canvas)
        {
            IEnumerable<IDrawable> drawables = cannons
                .Concat<IDrawable>(projectiles)
                .Concat(zombies);
            foreach (IDrawable drawable in drawables)
                drawable.Draw(canvas);
        }

        public IEnumerator<Zombie> GetEnumerator()
            => zombies.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
