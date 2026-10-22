namespace Application
{
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
}
