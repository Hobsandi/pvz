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
    public class Magazine : IEnumerable<ISpell>
    {
        private List<ISpell> spells;


        public IEnumerator<ISpell> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        // coolt namn
        public void Load(ISpell spell) => spells.Add(spell);
        

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}