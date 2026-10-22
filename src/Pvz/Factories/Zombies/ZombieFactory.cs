namespace Application
{
    // Gemensam bas: subklasserna bestämmer bara rörelse och utseende.
    // Rörelsen skapas ny för varje zombie eftersom den kan ha state.
    public abstract class ZombieFactory(PlayerBase playerBase, KillCounter kills)
        : IZombieFactory
    {
        public Zombie Create(Position start, int health)
            => new(start, health, CreateMovement(), Sprite, playerBase, kills);

        protected abstract IMovement CreateMovement();

        protected abstract ISprite Sprite { get; }
    }
}
