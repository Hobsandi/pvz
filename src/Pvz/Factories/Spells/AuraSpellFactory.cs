namespace Application
{
    public class AuraSpellFactory(Board board, IEffectFactory effectFactory)
        : ISpellFactory
    {
        public ISpell GenerateSpell()
            => new AuraSpell(effectFactory.GenerateEffect(), board);
    }
}
