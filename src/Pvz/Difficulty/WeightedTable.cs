namespace Application
{
    // Slumpar fram ett objekt där varje objekt har en vikt. Vikt 6 kommer
    // tre gånger så ofta som vikt 2, och vikt 0 kommer aldrig.
    // Vikterna kan ändras under rundan med SetWeight.
    public class WeightedTable<T>(Random rand)
    {
        private readonly List<WeightedItem<T>> items = new List<WeightedItem<T>>();

        public void Add(T value, int weight)
            => items.Add(new WeightedItem<T>(value, weight));

        // Finns värdet inte än läggs det till
        public void SetWeight(T value, int weight)
        {
            WeightedItem<T>? item = items.Find(item => item.Holds(value));
            if (item == null)
                Add(value, weight);
            else
                item.SetWeight(weight);
        }

        // Ökar vikten med amount. Finns värdet inte än läggs det till.
        public void AddWeight(T value, int amount)
        {
            WeightedItem<T>? item = items.Find(item => item.Holds(value));
            if (item == null)
                Add(value, amount);
            else
                item.AddWeightTo(amount);
        }

        // Vikterna ligger som sträckor efter varandra:
        //   [0 1 2 3 4 5][6 7 8][9]   vikter 6, 3, 1 (totalt 10)
        // Slumpa ett tal under totalen och dra av en vikt i taget.
        // Det objekt som får talet under 0 är det som väljs.
        public T Pick()
        {
            int total = 0;
            foreach (WeightedItem<T> item in items)
                total = item.AddWeightTo(total);

            if (total == 0)
                throw new InvalidOperationException("Cannot pick: the table is empty or all weights are 0");

            int roll = rand.Next(total);
            foreach (WeightedItem<T> item in items)
                if (item.TryPick(ref roll, out T picked))
                    return picked;

            // Kan inte hända så länge totalen stämmer
            throw new InvalidOperationException("Pick failed");
        }
    }
}
