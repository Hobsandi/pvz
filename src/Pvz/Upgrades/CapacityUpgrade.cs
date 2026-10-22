namespace Application
{
    public class CapacityUpgrade : IUpgrade
    {
        public ConsoleColor Color => ConsoleColor.Yellow;

        public void Apply(IUpgradeContext context)
            => context.ChooseCannon("Give +1 spell slot to which cannon?",
                cannon => cannon.CanGrow).IncreaseCapacity();

        public string Describe() => "+1 spell slot on a cannon";
    }
}
