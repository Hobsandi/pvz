namespace Application
{
    // Ger en uppgradering vid växande antal kills: 10, 25, 45, 70...
    // OnKill körs mitt i board.Update, så här noteras bara att en
    // uppgradering väntar. GameRunner hämtar den efter ticken.
    public class Upgrader(ISpellFactory spellFactory, MenuInput input)
        : IKillObserver
    {
        const int SPELL_CHOICES = 3;

        int nextUpgradeAt = 10;
        int step = 10;
        int pending = 0;

        public void OnKill(int kills)
        {
            if (kills < nextUpgradeAt) return;
            pending++;
            step += 5;
            nextUpgradeAt += step;
        }

        public bool TryTakePending()
        {
            if (pending == 0) return false;
            pending--;
            return true;
        }

        // Visar tre nya spells och returnerar den spelaren väljer
        public ISpell ChooseSpell()
        {
            List<ISpell> spells = [];
            for (int i = 0; i < SPELL_CHOICES; i++)
                spells.Add(spellFactory.GenerateSpell());
            return input.Choose(
                new Menu<ISpell>("Upgrade! Pick a spell:", spells));
        }
    }
}
