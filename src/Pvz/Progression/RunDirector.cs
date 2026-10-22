namespace Application
{
    // Tillämpar nästa Stage när antalet kills når dess gräns.
    public class RunDirector : IKillObserver
    {
        readonly IReadOnlyList<Stage> stages;
        readonly SpawnSettings spawn;
        readonly RarityTable spells;
        int next = 0;

        public RunDirector(IReadOnlyList<Stage> stages, SpawnSettings spawn,
            RarityTable spells)
        {
            this.stages = stages;
            this.spawn = spawn;
            this.spells = spells;
            OnKill(0);
        }

        public void OnKill(int totalKills)
        {
            while (next < stages.Count && totalKills >= stages[next].AtKills)
                stages[next++].Apply(spawn, spells);
        }
    }
}
