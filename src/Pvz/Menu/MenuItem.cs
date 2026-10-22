namespace Application
{
    public class MenuItem<T>(string name, T value)
    {
        public string Name { get; set; } = name;
        public T Value { get; set; } = value;
    }
}