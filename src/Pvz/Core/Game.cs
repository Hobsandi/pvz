namespace Application
{
    // "Mastermind": äger huvudmenyn och bestämmer vad som händer i
    // vilken ordning. Själva spelandet sköter GameRunner.
    public class Game
    {
        public void Run()
        {
            Menu<IScreen> menu = new Menu<IScreen>("Main menu:",
            [
                new GameRunner(),
                new InformationScreen("Instructions","Welcome to Plants vs Zombies! The goal of the game is to defend your base from waves of zombies by strategically placing plants that attack the zombies. Each plant has unique abilities, and you must manage your resources wisely to survive each wave. Good luck!"),
                new QuitScreen()
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
