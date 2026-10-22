namespace Application
{
    public interface IEffect : IDescribable
    {
        void Apply(Zombie zombie);
    }
}
