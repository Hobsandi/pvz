namespace Application
{
    public class QuitScreen : IScreen
    {
        public string Describe() => "Quit";

        public void Display() => Console.WriteLine("Goodbye!");

        public void Run()
        {
            Display();
            Environment.Exit(0);
        }
    }
}
