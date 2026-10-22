namespace Application
{
    public class MichaelZombieFactory(PlayerBase playerBase, KillCounter kills)
        : ZombieFactory(playerBase, kills)
    {
        protected override IMovement CreateMovement() => new MichaelMovement();

        protected override ISprite Sprite { get; }
            = new SymbolSprite('M', ConsoleColor.DarkYellow);
    }
}
