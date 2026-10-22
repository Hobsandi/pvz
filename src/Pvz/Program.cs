using System.Text;

namespace Application
{
    class Program
    {
        static void Main(string[] args)
        {
            // Ramtecken och pilar kräver UTF-8
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;

            HighscoreStore highscores = new(
                Path.Combine(AppContext.BaseDirectory, "highscores.txt"));
            IScreen? screen = new MainMenuScreen(new KeyboardInput(), highscores);
            while (screen != null)
                screen = screen.Show();

            Console.Write("\x1b[0m\x1b[2J\x1b[H");
            Console.CursorVisible = true;
        }
    }
}
