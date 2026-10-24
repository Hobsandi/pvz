namespace Application
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu<IScreen> menu = new Menu<IScreen>("Main menu:",
            [
                new GameRunner(),
                new InformationScreen("Instructions","Welcome to Plants vs Zombies! The goal of the game is to defend your base from waves of zombies by strategically placing plants that attack the zombies. Each plant has unique abilities, and you must manage your resources wisely to survive each wave. Good luck!"),
                new QuitScreen()
            ]);

            // Tillbaka till menyn efter varje skärm. QuitScreen avslutar.
            MenuInput input = new();
            while (true)
            {
                IScreen screen = input.Choose(menu);
                Console.Clear();
                screen.Run();
            }
        }
    }
}
