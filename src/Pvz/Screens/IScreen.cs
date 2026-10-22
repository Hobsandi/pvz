namespace Application
{
    // State: varje skärm körs tills den är klar och returnerar nästa
    // skärm, eller null för att avsluta. Describe() är texten i huvudmenyn.
    public interface IScreen : IDescribable
    {
        IScreen? Show();
    }
}
