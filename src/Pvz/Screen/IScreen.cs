namespace Application
{
    public interface IScreen : IDescribable
    {
        // Ritar skärmen en gång
        public void Display();

        // Kör skärmen tills den är klar, sedan tillbaka till menyn
        public void Run();
    }
}
