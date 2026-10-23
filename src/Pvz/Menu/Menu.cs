namespace Application
{
    // PLATS FÖR ETT KRAV
    // Vi behöver en ny klass för olika sorters menyer, olika inputhandling?
    public class Menu<T>(string title, IReadOnlyList<MenuItem<T>> items) where T : IDescribable 
    {
        private int index = 0;
        public T Select() => items[index].Value;
        public void Next()
        {
            index = (index + 1) % items.Count;
        }
        public void Previous()
        {
            index = (index - 1 + items.Count) % items.Count;
        }
        // Visar menyn och väntar tills spelaren väljer med Enter
        public T Choose()
        {
            while (true)
            {
                Console.Clear();
                Display();

                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.DownArrow) Next();
                else if (key == ConsoleKey.UpArrow) Previous();
                else if (key == ConsoleKey.Enter) return Select();
            }
        }

        public void Display()
        {
            Console.WriteLine(title);
            for (int i = 0; i < items.Count; i++)
            {
                if (i == index)
                {
                    Console.Write("> ");
                }
                else
                {
                    Console.Write("  ");
                }
                Console.WriteLine($"{i + 1}. {items[i].Name}");
            }
        }

    }
}