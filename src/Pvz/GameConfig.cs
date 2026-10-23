namespace Application
{

    /*
    FRÅGA:
    statisk klass. OK?
    
    */
    public static class GameConfig
    {
        public const int PIXEL_SIZE = 10;
        public const int ROWS = 4;
        public const int COLUMNS = 40;
        public const int RIGHT_EDGE = (COLUMNS - 1) * PIXEL_SIZE;
        public const int TICK_MS = 50;
        public const int BASE_HEALTH = 3;
        public const int ZOMBIE_HEALTH = 100;
        public const int CANNON_TICKS = 20;
        public const int ZOMBIE_TICKS = 6;
        public const int SPAWN_TICKS = 30;
        public const int BASIC_SPELL_DAMAGE = 25;
        public const int SPELL_TICKS_PER_STEP = 2;
    }
}
