namespace Application
{
    // Istället för att ha en statisk klass
    public class Settings
    {
        public int BaseHealth { get; set; } = 3;
        public int ZombieHealth { get; set; } = 100;
        public int CannonTicks { get; set; } = 20;
        public int ZombieTicks { get; set; } = 6;
        public int SpawnTicks { get; set; } = 30;
    }
}
