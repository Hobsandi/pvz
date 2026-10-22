namespace Application
{
    // Slumpar fram ett objekt där varje objekt har en vikt. Vikt 0 betyder
    // att objektet aldrig väljs, och vikterna kan ändras under en runda.
    public class WeightedTable<T> where T : notnull
    {
        readonly List<T> items = [];
        readonly List<int> weights = [];

        public static WeightedTable<T> Uniform(IEnumerable<T> items)
        {
            WeightedTable<T> table = new();
            foreach (T item in items) table.Add(item, 1);
            return table;
        }

        public WeightedTable<T> Add(T item, int weight)
        {
            items.Add(item);
            weights.Add(weight);
            return this;
        }

        public void SetWeight(T item, int weight)
        {
            int index = items.IndexOf(item);
            if (index < 0) Add(item, weight);
            else weights[index] = weight;
        }

        public T Pick()
        {
            int roll = Random.Shared.Next(weights.Sum());
            for (int i = 0; i < items.Count; i++)
            {
                roll -= weights[i];
                if (roll < 0) return items[i];
            }
            throw new InvalidOperationException(
                "WeightedTable is empty or all weights are 0");
        }
    }
}
