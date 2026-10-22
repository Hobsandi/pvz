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
        // Värden som kan ändras mellan rundor ligger i Settings
    }
}
