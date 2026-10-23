namespace Application
{
    // Ett värde och dess vikt. Vikten och värdet är privata, så tabellen
    // frågar objektet i stället för att läsa dem direkt.
    public class WeightedItem<T>
    {
        private readonly T value;
        private int weight;

        public WeightedItem(T value, int weight)
        {
            this.value = value;
            SetWeight(weight);
        }

        public void SetWeight(int newWeight)
        {
            if (newWeight < 0)
                throw new ArgumentOutOfRangeException(nameof(newWeight), "Weight cannot be negative");
            weight = newWeight;
        }

        // Går via SetWeight så att vikten aldrig kan bli negativ
        public void IncreaseWeight(int amount) => SetWeight(weight + amount);

        // Är det här objektet för just det här värdet?
        public bool Holds(T other) => EqualityComparer<T>.Default.Equals(value, other);

        // Används för att räkna ihop totalen
        public int AddWeightTo(int total) => total + weight;

        // Drar av vikten från roll. Hamnar roll under 0 är det det här
        // objektet som valts, och värdet lämnas ut genom picked.
        public bool TryPick(ref int roll, out T picked)
        {
            roll -= weight;
            picked = value;
            return roll < 0;
        }
    }
}
