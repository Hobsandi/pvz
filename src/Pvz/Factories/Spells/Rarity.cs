namespace Application
{
    public class Rarity(string name, ConsoleColor color)
    {
        public static readonly Rarity Common = new("Common", ConsoleColor.Gray);
        public static readonly Rarity Rare = new("Rare", ConsoleColor.Cyan);
        public static readonly Rarity Epic = new("Epic", ConsoleColor.Magenta);

        public string Name => name;
        public ConsoleColor Color => color;
    }
}
