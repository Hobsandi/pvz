namespace Application
{
    // Väljer slumpmässigt en av de inskickade fabrikerna för varje zombie.
    // Är själv en IZombieFactory, så spawnern märker ingen skillnad.
    public class RandomZombieFactory(IReadOnlyList<IZombieFactory> factories) : IZombieFactory
    {
        public Zombie GenerateZombie(Position position, int health)
            => factories[Random.Shared.Next(factories.Count)].GenerateZombie(position, health);
    }
}
