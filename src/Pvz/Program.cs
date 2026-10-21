using System.Collections;
using System.Text;

namespace Application
{
    class Program
    {
        const int PIXEL_SIZE = 10;
        const int ROWS = 4;
        const int COLUMNS = 40;
        const int RIGHT_EDGE = (COLUMNS - 1) * PIXEL_SIZE;
        const int TICK_MS = 50;
        const int CANNON_TICKS = 20;
        const int ZOMBIE_TICKS = 6;
        const int SPAWN_TICKS = 30;
        const int ZOMBIE_HEALTH = 10;
        const int BASE_HEALTH = 3;

        static void Main(string[] args)
        {
            Board board = new();
            PlayerBase playerBase = new(BASE_HEALTH);
            List<ISpell> spells =
            [
                new ProjectileSpell([new DamageEffect(3)], board, 1,
                    new SingleImpact(), new TrailSprite('>', '-')),

                new ProjectileSpell([new FreezeEffect(4)], board, 2,
                    new PiercingImpact(), new SymbolSprite('*')),

                new ProjectileSpell([new DamageEffect(2)], board, 2,
                    new ExplosiveImpact(board, 1), new SymbolSprite('o')),

                    
                new BeamSpell([new DamageEffect(1)], board),
                new AuraSpell([new PoisonEffect(board, 1, 5, 10)], board),
            ];
            for (int row = 0; row < ROWS; row++)
            {
                Position position = new(0, row);
                Cannon cannon = new(position, new SymbolSprite('C'));
                spells.ForEach(cannon.AddSpell);
                board.Add(cannon, CANNON_TICKS);
            }
            ZombieSpawner spawner = new(board, playerBase, new Random());
            board.Register(spawner, SPAWN_TICKS);
            new Game(board, playerBase, new Canvas(COLUMNS, ROWS)).Run(TICK_MS);
        }

        public interface IUpdater
        {
            void Update();
            bool IsAlive();
        }

        public interface IWorld
        {
            void Register(IUpdater updater, int ticksPerUpdate);
        }

        public interface IDrawable
        {
            void Draw(Canvas canvas);
        }

        public interface IDescribable
        {
            string Describe();
        }

        public interface IEffect : IDescribable
        {
            void Apply(Zombie zombie);
        }

        // FALL #1:
        // VAD: Bridge pattern.
        // HUR: ISpell (Beam, Aura, Projectile) är komponerade med IEffect
        // (Damage, Freeze, Poison). Alla former kan bära alla effekter.
        // VARFÖR: Nya former och nya effekter läggs till oberoende av
        // varandra, utan en klass per kombination.
        public interface ISpell : IDescribable
        {
            void Cast(Position origin);
        }

        public interface IMovement
        {
            Position Move(Position from);
        }

        // FALL #3:
        // VAD: Strategy pattern.
        // HUR: Projectile får en IImpact injicerad (Single, Piercing,
        // Explosive) som avgör vad som händer vid träff.
        // VARFÖR: Nya träffbeteenden kräver ingen ändring i Projectile
        // och inga villkor på projektiltyp.
        public interface IImpact
        {
            // Returnerar true om projektilen ska fortsätta flyga.
            bool Strike(Zombie target, Position at, IEffect effect);
        }

        public interface ISprite
        {
            void Draw(Canvas canvas, Position at);
        }

        public readonly struct Position(int x, int row)
            : IComparable<Position>
        {
            readonly int x = x;
            readonly int row = row;

            public Position Shift(int dx) => new(x + dx, row);

            public Position MoveToRow(int newRow) => new(x, newRow);

            public bool IsOnSameRowAs(Position other) => row == other.row;

            public bool IsAtOrPast(Position other)
                => IsOnSameRowAs(other) && x >= other.x;

            public bool IsWithin(Position other, int radius)
                => Math.Abs(x - other.x) <= radius * PIXEL_SIZE
                    && Math.Abs(row - other.row) <= radius;

            public bool IsAtLeftEdge() => x <= 0;

            public bool IsPastRightEdge() => x > RIGHT_EDGE;

            // Unsure about this one
            public void Plot(Canvas canvas, char symbol)
                => canvas.Plot(x / PIXEL_SIZE, row, symbol);

            public int CompareTo(Position other) => x.CompareTo(other.x);
        }

        public class Delayer(IUpdater updater, int ticksPerUpdate) : IUpdater
        {
            int ticksSinceUpdate = 0;

            public void Update()
            {
                ticksSinceUpdate++;
                if (ticksSinceUpdate < ticksPerUpdate) return;

                ticksSinceUpdate = 0;
                updater.Update();
            }

            public bool IsAlive() => updater.IsAlive();
        }


        //Vi vill förstå varför generics används här och inte vanlig implementation av gränssnitt
        // FALL #2:
        // VAD: Generics.
        // HUR: EntityList<T> konstrueras med Zombie, Projectile, Cannon
        // och IUpdater. Constrainten behövs för Delayer och IsAlive().
        // VARFÖR: En implementation av lagring, uppdatering och städning
        // för alla sorters objekt, där Board ändå får tillbaka t.ex.
        // Zombie och kan anropa IsReachedBy.
        public class EntityList<T> : IEnumerable<T> where T : IUpdater
        {
            readonly List<T> items = [];
            readonly List<Delayer> delayers = [];
            readonly List<Delayer> pending = [];

            public void Add(T item, int ticksPerUpdate)
            {
                items.Add(item);
                pending.Add(new Delayer(item, ticksPerUpdate));
            }

            public void Update()
            {
                foreach (Delayer delayer in delayers)
                    delayer.Update();
                // Varför finns pending? 
                delayers.AddRange(pending);
                pending.Clear();
            }

            public void RemoveDead()
            {
                items.RemoveAll(item => !item.IsAlive());
                delayers.RemoveAll(delayer => !delayer.IsAlive());
            }

            public IEnumerator<T> GetEnumerator() => items.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

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

        public class Game(Board board, PlayerBase playerBase, Canvas canvas)
        {
            public void Run(int tickMs)
            {
                Console.Write("\x1b[2J");
                while (!playerBase.IsDestroyed())
                {
                    board.Update();
                    canvas.Clear();
                    board.Draw(canvas);
                    canvas.Render();
                    Console.WriteLine(playerBase.Describe());
                    Thread.Sleep(tickMs);
                }
                Console.WriteLine("Game over!");
            }
        }

        public class PlayerBase(int health) : IDescribable
        {
            int health = health;

            public void TakeHit() => health--;

            public bool IsDestroyed() => health <= 0;

            public string Describe() => $"Base HP: {health} ";
        }

        public class Canvas(int width, int height)
        {
            readonly char[,] cells = new char[height, width];

            public void Clear()
            {
                for (int row = 0; row < height; row++)
                    for (int x = 0; x < width; x++)
                        cells[row, x] = '.';
            }

            // Needs explaining
            public void Plot(int x, int row, char symbol)
            {
                if (x < 0 || x >= width || row < 0 || row >= height) return;
                cells[row, x] = symbol;
            }

            public void Render()
            {
                StringBuilder output = new("\x1b[H");
                for (int row = 0; row < height; row++)
                {
                    for (int x = 0; x < width; x++)
                        output.Append(cells[row, x]);
                    output.AppendLine();
                }
                Console.Write(output);
            }
        }
        //Explain different sprites
        public class SymbolSprite(char symbol) : ISprite
        {
            public void Draw(Canvas canvas, Position at)
                => at.Plot(canvas, symbol);
        }

        public class TrailSprite(char head, char tail) : ISprite
        {
            public void Draw(Canvas canvas, Position at)
            {
                at.Shift(-PIXEL_SIZE).Plot(canvas, tail);
                at.Plot(canvas, head);
            }
        }

        public class Cannon(Position position, ISprite sprite)
            : IUpdater, IDrawable
        {
            readonly List<ISpell> spells = [];
            int index = 0;

            public void AddSpell(ISpell spell) => spells.Add(spell);

            public void Update()
            {
                if (spells.Count == 0) return;

                spells[index].Cast(position);
                index = (index + 1) % spells.Count;
            }

            public bool IsAlive() => true;

            public void Draw(Canvas canvas) => sprite.Draw(canvas, position);
        }

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

        public class WalkingMovement : IMovement
        {
            public Position Move(Position from) => from.Shift(-PIXEL_SIZE);
        }

        public class TeleportingMovement(Random random, int stepsPerTeleport)
            : IMovement
        {
            int steps = 0;

            public Position Move(Position from)
            {
                steps++;
                Position next = from.Shift(-PIXEL_SIZE);
                if (steps % stepsPerTeleport != 0) return next;

                return next.MoveToRow(random.Next(ROWS));
            }
        }

        // Factory instead?
        public class ZombieSpawner(
            Board board, PlayerBase playerBase, Random random) : IUpdater
        {
            public void Update()
            {
                Position start = new(RIGHT_EDGE, random.Next(ROWS));
                Zombie zombie = random.Next(4) == 0
                    ? new Zombie(start, ZOMBIE_HEALTH,
                        new TeleportingMovement(random, 5),
                        new SymbolSprite('T'), playerBase)
                    : new Zombie(start, ZOMBIE_HEALTH,
                        new WalkingMovement(),
                        new SymbolSprite('Z'), playerBase);
                board.Add(zombie, ZOMBIE_TICKS);
            }

            public bool IsAlive() => true;
        }

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

        public class SingleImpact : IImpact
        {
            public bool Strike(Zombie target, Position at, IEffect effect)
            {
                effect.Apply(target);
                return false;
            }
        }

        // Goes through all enemies. Could we implement piercing that dies after a few hits?
        public class PiercingImpact : IImpact
        {
            public bool Strike(Zombie target, Position at, IEffect effect)
            {
                effect.Apply(target);
                return true;
            }
        }

        public class ExplosiveImpact(Board board, int radius) : IImpact
        {
            public bool Strike(Zombie target, Position at, IEffect effect)
            {
                foreach (Zombie zombie in board.FindZombiesNear(at, radius))
                    effect.Apply(zombie);
                return false;
            }
        }

        public class CompositeEffect(IReadOnlyList<IEffect> effects) : IEffect
        {
            public void Apply(Zombie zombie)
            {
                foreach (IEffect effect in effects)
                    effect.Apply(zombie);
            }

            public string Describe()
                => string.Join(" + ", effects.Select(e => e.Describe()));
        }

        public class DamageEffect(int amount) : IEffect
        {
            public void Apply(Zombie zombie) => zombie.TakeDamage(amount);

            public string Describe() => $"{amount} damage";
        }

        public class FreezeEffect(int steps) : IEffect
        {
            public void Apply(Zombie zombie) => zombie.Freeze(steps);

            public string Describe() => $"freeze for {steps} steps";
        }

        public class PoisonEffect(
            IWorld world,
            int damagePerDose,
            int doses,
            int ticksBetweenDoses) : IEffect
        {
            public void Apply(Zombie zombie)
            {
                PoisonTicker ticker = new(zombie, damagePerDose, doses);
                world.Register(ticker, ticksBetweenDoses);
            }

            public string Describe() => $"poison {damagePerDose} x {doses}";
        }

        public class PoisonTicker(Zombie zombie, int damagePerDose, int doses)
            : IUpdater
        {
            int dosesLeft = doses;

            public void Update()
            {
                zombie.TakeDamage(damagePerDose);
                dosesLeft--;
            }

            public bool IsAlive() => dosesLeft > 0 && zombie.IsAlive();
        }

        // Should we pass a composite effect to the constructor instead?
        public abstract class AreaSpell(IReadOnlyList<IEffect> effects)
            : ISpell
        {
            readonly IEffect effect = new CompositeEffect(effects);

            public void Cast(Position origin)
            {
                foreach (Zombie zombie in SelectTargets(origin))
                    effect.Apply(zombie);
            }

            public string Describe()
                => $"{DescribeShape()} of {effect.Describe()}";

            protected abstract IEnumerable<Zombie> SelectTargets(
                Position origin);

            protected abstract string DescribeShape();
        }

        public class BeamSpell(IReadOnlyList<IEffect> effects, Board board)
            : AreaSpell(effects)
        {
            protected override IEnumerable<Zombie> SelectTargets(
                Position origin)
                => board.FindZombiesInRowOf(origin);

            protected override string DescribeShape() => "Beam";
        }

        public class AuraSpell(IReadOnlyList<IEffect> effects, Board board)
            : AreaSpell(effects)
        {
            protected override IEnumerable<Zombie> SelectTargets(
                Position origin)
                //board är en ienumerable of zombie
                => board;

            protected override string DescribeShape() => "Aura";
        }

        public class ProjectileSpell(
            IReadOnlyList<IEffect> effects,
            Board board,
            int ticksPerStep,
            IImpact impact,
            ISprite sprite) : ISpell
        {
            readonly IEffect effect = new CompositeEffect(effects);

            public void Cast(Position origin)
            {
                Projectile projectile =
                    new(origin, effect, impact, sprite, board);
                board.Add(projectile, ticksPerStep);
            }

            public string Describe() => $"Projectile of {effect.Describe()}";
        }
    }
}
