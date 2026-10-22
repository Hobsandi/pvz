namespace Application
{
    public class WalkerZombieFactory(PlayerBase playerBase, KillCounter kills)
        : ZombieFactory(playerBase, kills)
    {
        protected override IMovement CreateMovement() => new WalkingMovement();

        protected override ISprite Sprite { get; }
            = new SymbolSprite('Z', ConsoleColor.Green);
    }
}
