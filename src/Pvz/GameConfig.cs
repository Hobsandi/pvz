namespace Application
{
    public static class GameConfig
    {
        public const int PIXEL_SIZE = 10;
        public const int COLUMNS = 40;
        public const int RIGHT_EDGE = (COLUMNS - 1) * PIXEL_SIZE;
        public const int TICK_MS = 50;

        public const int START_LANES = 4;
        public const int MAX_LANES = 6;
        public const int KILLS_PER_LANE = 20;

        public const int CANNON_TICKS = 20;
        public const int START_CAPACITY = 2;
        public const int MAX_CAPACITY = 5;
        public const int BASIC_DAMAGE = 25;

        public const int ZOMBIE_TICKS = 6;
        public const int SPAWN_TICKS = 30;
        public const int ZOMBIE_HEALTH = 100;

        public const int BASE_HEALTH = 3;
        public const int HEAL_AMOUNT = 1;

        // Första uppgraderingen vid 5 kills, sedan växer avståndet x1.5
        public const int FIRST_UPGRADE_AT = 5;
        public const double UPGRADE_GROWTH = 1.5;
    }
}
