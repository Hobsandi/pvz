using static Application.GameConfig;

namespace Application
{
    // Fyra val: tre spells och ett fjärde som är heal eller +1 slot.
    public class UpgradeOffer(RarityTable spells, Board board, PlayerBase playerBase)
    {
        public IReadOnlyList<IUpgrade> Generate()
            => [spells.Generate(), spells.Generate(), spells.Generate(), Fourth()];

        IUpgrade Fourth()
        {
            bool canHeal = playerBase.IsDamaged();
            bool canGrow = board.Cannons.Any(cannon => cannon.CanGrow);

            if (canHeal && canGrow)
                return Random.Shared.Next(2) == 0
                    ? new HealUpgrade(HEAL_AMOUNT)
                    : new CapacityUpgrade();
            if (canHeal) return new HealUpgrade(HEAL_AMOUNT);
            if (canGrow) return new CapacityUpgrade();
            // Inget av dem är meningsfullt, så en spell till
            return spells.Generate();
        }
    }
}
