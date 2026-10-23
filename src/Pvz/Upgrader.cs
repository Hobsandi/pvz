namespace Application
{
    // Ger en uppgradering vid växande antal kills: 10, 25, 45, 70...
    // OnKill körs mitt i board.Update, så här noteras bara att en
    // uppgradering väntar. GameRunner kör den efter ticken, och spelet
    // står still medan menyn är öppen.
    public class Upgrader(Difficulty difficulty, Board board) : IKillObserver
    {
        const int SPELL_CHOICES = 3;

        int nextUpgradeAt = 1;
        int step = 10;
        int pending = 0;

        public void OnKill(int kills)
        {
            if (kills < nextUpgradeAt) return;
            pending++;
            step += 5;
            nextUpgradeAt += step;
        }

        // En metod i stället för en property
        public bool TryTakePending()
        {
            if (pending == 0) return false;
            pending--;
            return true;
        }

        // Välj en spell, välj en kanon, lägg spellen på kanonen
        public void RunUpgrade()
        {
            ISpell spell = ChooseSpell();
            ChooseCannon(spell).AddSpell(spell);
        }

        ISpell ChooseSpell()
        {
            List<MenuItem<ISpell>> items = new();
            for (int i = 0; i < SPELL_CHOICES; i++)
            {
                ISpell spell = difficulty.GenerateSpell();
                items.Add(new MenuItem<ISpell>(spell.Describe(), spell));
            }
            return new Menu<ISpell>("Upgrade! Pick a spell:", items).Choose();
        }

        Cannon ChooseCannon(ISpell spell)
        {
            List<MenuItem<Cannon>> items = new();
            int lane = 1;
            foreach (Cannon cannon in board.Cannons())
                items.Add(new MenuItem<Cannon>($"Lane {lane++}: {cannon.Describe()}", cannon));
            return new Menu<Cannon>($"Which cannon gets \"{spell.Describe()}\"?", items).Choose();
        }
    }
}
