using static Application.GameConfig;

namespace Application
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new();
            PlayerBase playerBase = new(BASE_HEALTH);
            List<ISpell>[] loadouts =
            [
                [
                    new ProjectileSpell([new DamageEffect(3)], board, 1,
                        new SingleImpact(), new TrailSprite('>', '-')),
                ],
                [
                    new ProjectileSpell([new FreezeEffect(4)], board, 2,
                        new PiercingImpact(), new SymbolSprite('*')),
                    new BeamSpell([new DamageEffect(2)], board),
                ],
                [
                    new ProjectileSpell([new DamageEffect(2)], board, 2,
                        new ExplosiveImpact(board, 1), new SymbolSprite('o')),
                    new ProjectileSpell(
                        [new DamageEffect(1),
                            new PoisonEffect(board, 1, 3, 10)],
                        board, 3,
                        new ExplosiveImpact(board, 1), new SymbolSprite('@')),
                ],
                [
                    new AuraSpell([new PoisonEffect(board, 1, 5, 10)], board),
                    new BeamSpell(
                        [new DamageEffect(1), new FreezeEffect(2)], board),
                ],
            ];
            for (int row = 0; row < ROWS; row++)
            {
                Position position = new(0, row);
                Cannon cannon = new(position, new SymbolSprite('C'));
                loadouts[row].ForEach(cannon.AddSpell);
                board.Add(cannon, CANNON_TICKS);
            }
            ZombieSpawner spawner = new(board, playerBase, new Random());
            board.Register(spawner, SPAWN_TICKS);
            new Game(board, playerBase, new Canvas(COLUMNS, ROWS)).Run(TICK_MS);
        }
    }
}
