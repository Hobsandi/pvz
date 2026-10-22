namespace Application
{
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
}
