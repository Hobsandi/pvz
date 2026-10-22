using System.Collections;

namespace Application
{
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

        // Levande lista, t.ex. för menyer som ska se nya kanoner direkt
        public IReadOnlyList<T> Items => items;

        public void Add(T item, int ticksPerUpdate)
        {
            items.Add(item);
            pending.Add(new Delayer(item, ticksPerUpdate));
        }

        public void Update()
        {
            foreach (Delayer delayer in delayers)
                delayer.Update();
            // Saker som läggs till under Update (t.ex. en projektil som en
            // kanon skjuter, eller en ny bana mitt i en tick) hamnar i
            // pending. Att lägga till i delayers medan foreach går över
            // den ger "Collection was modified".
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
}
