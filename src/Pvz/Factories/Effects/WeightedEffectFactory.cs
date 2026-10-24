namespace Application
{
    // Skapar en slumpad effekt: låg skada, frys, gift eller hög skada.
    // Varje sort har en vikt som avgör hur ofta den kommer, och hög skada
    // är ovanligast.
    public class WeightedEffectFactory : IEffectFactory
    {
        const int LOW_DAMAGE_WEIGHT = 6;
        const int FREEZE_WEIGHT = 3;
        const int POISON_WEIGHT = 2;
        const int HIGH_DAMAGE_WEIGHT = 1;

        readonly WeightedTable<IEffectFactory> effects;

        public WeightedEffectFactory(Board board, int zombieHealth, Random rand)
        {
            effects = new WeightedTable<IEffectFactory>(rand);
            effects.Add(new LowDamageEffectFactory(zombieHealth),
                LOW_DAMAGE_WEIGHT);
            effects.Add(new FreezeEffectFactory(), FREEZE_WEIGHT);
            effects.Add(new PoisonEffectFactory(board), POISON_WEIGHT);
            effects.Add(new HighDamageEffectFactory(zombieHealth),
                HIGH_DAMAGE_WEIGHT);
        }

        public IEffect GenerateEffect() => effects.Pick().GenerateEffect();
    }
}
