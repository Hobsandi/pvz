namespace Application
{
    // Håller reda på vilket alternativ som är valt och kan visa sig själv.
    // Tangenterna sköts av MenuInput, så menyn vet inget om tangentbordet.
    public class Menu<T>(string title, List<T> items)
        where T : IDescribable
    {
        private int index = 0;
        public T Select() => items[index];
        public void Next()
        {
            index = (index + 1) % items.Count;
        }
        public void Previous()
        {
            index = (index - 1 + items.Count) % items.Count;
        }

        public void Add(T item)
        {
            items.Add(item);
        }

        public void DrawCursor(ICanvas canvas)
        {
            canvas.Plot(0,index,'>');
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
                Console.WriteLine($"{i + 1}. {items[i].Describe()}");
            }
        }

    }
}