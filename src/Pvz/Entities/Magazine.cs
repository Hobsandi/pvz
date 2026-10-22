using System.Collections;

namespace Application
{
    public class Magazine : IMagazine
    {
        readonly List<ISpell> spells = [];
        readonly IEnumerator<ISpell?> firingOrder;
        readonly int maxCapacity;
        int capacity;

        public Magazine(int capacity, int maxCapacity)
        {
            this.capacity = capacity;
            this.maxCapacity = maxCapacity;
            firingOrder = FiringOrder().GetEnumerator();
        }

        public bool IsFull => spells.Count >= capacity;
        public bool CanGrow => capacity < maxCapacity;

        // Coolt namn. Fullt magasin: första spellen åker ut.
        public void Load(ISpell spell)
        {
            if (IsFull) spells.RemoveAt(0);
            spells.Add(spell);
        }

        public void IncreaseCapacity()
        {
            if (CanGrow) capacity++;
        }

        public ISpell? Next()
        {
            firingOrder.MoveNext();
            return firingOrder.Current;
        }

        // En egen oändlig iterator som har sitt eget index och läser listan
        // på nytt varje steg. List<T>:s inbyggda enumerator kastar ett
        // undantag om listan ändras, och Load ändrar den mitt i spelet.
        IEnumerable<ISpell?> FiringOrder()
        {
            for (int i = 0; ; i++)
                yield return spells.Count == 0 ? null : spells[i % spells.Count];
        }

        public IEnumerator<ISpell> GetEnumerator() => spells.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public string Describe() => $"Slots {spells.Count}/{capacity}";
    }
}
