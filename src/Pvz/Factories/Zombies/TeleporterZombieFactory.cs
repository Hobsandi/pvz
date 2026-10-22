namespace Application
{
    public class TeleporterZombieFactory(Board board, PlayerBase playerBase,
        KillCounter kills) : ZombieFactory(playerBase, kills)
    {
        protected override IMovement CreateMovement()
            => new TeleportingMovement(Random.Shared, 5, board);

        protected override ISprite Sprite { get; }
            = new SymbolSprite('T', ConsoleColor.Magenta);
    }
}
