namespace Application
{
    public record Score(string Name, int Kills);

    // Topp 10 sparas som rader "namn;kills". Saknas filen eller är en rad
    // trasig hoppas den över, så spelet startar alltid.
    public class HighscoreStore(string path)
    {
        const int MAX_SCORES = 10;
        public const int MAX_NAME_LENGTH = 16;

        public IReadOnlyList<Score> Top() => Load();

        public int Best() => Load().FirstOrDefault()?.Kills ?? 0;

        public bool Qualifies(int kills)
        {
            List<Score> scores = Load();
            return kills > 0
                && (scores.Count < MAX_SCORES || kills > scores[^1].Kills);
        }

        public void Add(string name, int kills)
        {
            name = name.Replace(";", "").Trim();
            if (name.Length > MAX_NAME_LENGTH) name = name[..MAX_NAME_LENGTH];
            if (name.Length == 0) return;

            List<Score> scores = Load();
            scores.Add(new Score(name, kills));
            File.WriteAllLines(path, scores
                .OrderByDescending(score => score.Kills)
                .Take(MAX_SCORES)
                .Select(score => $"{score.Name};{score.Kills}"));
        }

        List<Score> Load()
        {
            if (!File.Exists(path)) return [];

            List<Score> scores = [];
            foreach (string line in File.ReadAllLines(path))
            {
                string[] parts = line.Split(';');
                if (parts.Length == 2 && int.TryParse(parts[1], out int kills))
                    scores.Add(new Score(parts[0], kills));
            }
            return [.. scores.OrderByDescending(score => score.Kills)];
        }
    }
}
