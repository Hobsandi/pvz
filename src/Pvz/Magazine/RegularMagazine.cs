using System.Collections;
/*
VI GÖR OM MAGAZINE TILL ETT GRÄNSSNITT
CANON TAR IN ETT IMAGAZINE
Olika magasin implementerar GetEnumerator på olika sätt, t.ex.
kör varje spell två gånger
första, sen sista, sen andra, näst sista osv.

kanske skiter vi i det helt och hållet



*/


namespace Application
{
    public class RegularMagazine(int capacity, int maxCapacity) : IMagazine
    {
        private List<ISpell> spells = new List<ISpell>();
        int index = 0;


        public IEnumerator<ISpell> GetEnumerator()
        {
            return spells.GetEnumerator();
        }

        public void Load(ISpell spell) => spells.Add(spell);
        
        public void Upgrade()
        {
            if (capacity < maxCapacity)
                capacity++;
        }

        public void Cast(Position position)
        {
            if (spells.Count == 0) return;

            spells[index].Cast(position);
            // Kanske blir ett problem sen
            index = (index + 1) % spells.Count;
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}