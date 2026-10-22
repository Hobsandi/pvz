namespace Application
{
    public class BeamSpellFactory(Board board, IEffectFactory effectFactory)
        : ISpellFactory
    {
        public ISpell GenerateSpell()
            => new BeamSpell(effectFactory.GenerateEffect(), board);
    }
}
