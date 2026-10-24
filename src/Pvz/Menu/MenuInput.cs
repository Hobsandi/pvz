namespace Application
{
    // Översätter tangenttryck till anrop på en meny. Menyn själv vet inget
    // om tangentbordet, så samma meny kan styras både som en lista som
    // väntar på val och mitt i spelet utan att spelet stannar.
    public class MenuInput
    {
        // Uppåt och nedåt flyttar valet. Returnerar true vid Enter.
        public bool Handle<T>(Menu<T> menu, ConsoleKey key)
            where T : IDescribable
        {
            if (key == ConsoleKey.UpArrow) menu.Previous();
            else if (key == ConsoleKey.DownArrow) menu.Next();
            return key == ConsoleKey.Enter;
        }

        // Visar menyn som en lista och väntar tills spelaren väljer
        public T Choose<T>(Menu<T> menu) where T : IDescribable
        {
            while (true)
            {
                Console.Clear();
                menu.Display();
                if (Handle(menu, Console.ReadKey(true).Key))
                    return menu.Select();
            }
        }
    }
}
