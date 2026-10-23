namespace Application
{
    // En sprite vet själv hur den ser ut, både tecken och färg.
    // Den som ritar behöver bara säga var.
    public interface ISprite
    {
        void Draw(ICanvas canvas, Position at);
    }
}
