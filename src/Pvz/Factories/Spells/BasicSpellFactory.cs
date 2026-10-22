using static Application.GameConfig;

namespace Application
{
    // Startspellen som alla kanoner får, även på nya banor.
    public class BasicSpellFactory(Board board) : ISpellFactory
    {
        public ISpell GenerateSpell()
            => new ProjectileSpell(new DamageEffect(BASIC_DAMAGE), board, 2,
                new SingleImpact(), new TrailSprite('>', '-', ConsoleColor.Yellow));
    }
}
