
using UnityEngine;
using System.Collections.Generic;

public class StatModule : MonoBehaviour
{
    [SerializeField] private Dictionary<StatType, Stat> stats;

    public Stat this[StatType type]
    {
        get
        {
            stats.TryGetValue(type, out Stat stat);
            return stat;
        }
    }
}
