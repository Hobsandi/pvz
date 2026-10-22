namespace Application
{
    public interface IUpgradeContext
    {
        PlayerBase PlayerBase { get; }

        // Låter spelaren välja en kanon. Bara kanoner där canChoose
        // är sann går att bekräfta.
        Cannon ChooseCannon(string prompt, Predicate<Cannon> canChoose);
    }
}
