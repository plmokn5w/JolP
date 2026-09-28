
using System.Collections.Generic;

public class StatModule : Module
{
    private Dictionary<StatType, Stat> stats;

    public void Initialize(StatModuleData data)
    {
        stats = new Dictionary<StatType, Stat>();
        for (int i = 0; i < data.stats.Count; i++)
            stats.Add(data.stats[i].type, new Stat(data.stats[i].value));
    }

    public Stat this[StatType type]
    {
        get
        {
            stats.TryGetValue(type, out Stat stat);
            return stat;
        }
    }
}
