using static Application.GameConfig;

namespace Application
{
    // All balansering av en runda på ett ställe. Varje steg ändrar
    // spawnandet och hur ofta varje sällsynthet erbjuds.
    public static class Stages
    {
        public static IReadOnlyList<Stage> Create(IZombieFactory walker,
            IZombieFactory michael, IZombieFactory teleporter) =>
        [
            new(0, (spawn, spells) =>
            {
                spawn.Zombies.SetWeight(walker, 1);
                spells.SetWeight(Rarity.Common, 80);
                spells.SetWeight(Rarity.Rare, 18);
                spells.SetWeight(Rarity.Epic, 2);
            }),
            new(15, (spawn, _) =>
            {
                spawn.Zombies.SetWeight(michael, 1);
            }),
            new(30, (spawn, spells) =>
            {
                spawn.Zombies.SetWeight(teleporter, 1);
                spawn.Interval = SPAWN_TICKS * 4 / 5;
                spells.SetWeight(Rarity.Rare, 30);
                spells.SetWeight(Rarity.Epic, 6);
            }),
            new(60, (spawn, spells) =>
            {
                spawn.Health = ZOMBIE_HEALTH * 3 / 2;
                spawn.ZombiesPerWave = 2;
                spells.SetWeight(Rarity.Rare, 35);
                spells.SetWeight(Rarity.Epic, 12);
            }),
            new(100, (spawn, spells) =>
            {
                spawn.Interval = SPAWN_TICKS * 3 / 5;
                spawn.Health = ZOMBIE_HEALTH * 2;
                spawn.Zombies.SetWeight(teleporter, 2);
                spells.SetWeight(Rarity.Epic, 20);
            }),
        ];
    }
}
