namespace Application
{
    // "Mastermind": äger inställningarna och huvudmenyn, och bestämmer
    // vad som händer i vilken ordning. Själva spelandet sköter GameRunner.
    public class Game
    {
        readonly Settings settings = new();

        public void Run()
        {
            Menu<IScreen> menu = new Menu<IScreen>("Main menu:",
            [
                new MenuItem<IScreen>("Start game", new GameRunner(settings)),
                new MenuItem<IScreen>("Instructions", new InformationScreen("Instructions","Welcome to Plants vs Zombies! The goal of the game is to defend your base from waves of zombies by strategically placing plants that attack the zombies. Each plant has unique abilities, and you must manage your resources wisely to survive each wave. Good luck!")),
                new MenuItem<IScreen>("Quit", new QuitScreen())
            ]);

            ConsoleKey key;
            do
            {
                Console.Clear();
                menu.Display();

                key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.DownArrow) menu.Next();
                else if (key == ConsoleKey.UpArrow) menu.Previous();
                else if (key == ConsoleKey.Enter) {
                    Console.Clear(); 
                    menu.Select().Run();
                }
                    
            } while (key != ConsoleKey.Escape);
        }
    }
}
