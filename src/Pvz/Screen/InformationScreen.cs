namespace Application
{
    public class InformationScreen(string desriptor, string info) : IScreen
    {
        public string Describe() => desriptor;
        public void Display()
        {
            Console.WriteLine(info);
        }
        public void Run()
        {
            Display();
            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
        }
    }
}