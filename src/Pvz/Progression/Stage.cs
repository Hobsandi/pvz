namespace Application
{
    public record Stage(int AtKills, Action<SpawnSettings, RarityTable> Apply);
}
