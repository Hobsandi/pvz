namespace Application
{
    public static class GameConfig
    {
        public const int PIXEL_SIZE = 10;
        public const int ROWS = 4;
        public const int COLUMNS = 40;
        public const int RIGHT_EDGE = (COLUMNS - 1) * PIXEL_SIZE;
        public const int TICK_MS = 50;
        public const int CANNON_TICKS = 20;
        public const int ZOMBIE_TICKS = 6;
        public const int SPAWN_TICKS = 30;
        public const int ZOMBIE_HEALTH = 10;
        public const int BASE_HEALTH = 3;
    }
}
