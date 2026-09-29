
using UnityEngine;
using System.Collections.Generic;

public class StatModule : MonoBehaviour
{
    [SerializeField] private Dictionary<StatType, float> stats;

    public float this[StatType type]
    {
        get
        {
            stats.TryGetValue(type, out float stat);
            return stat;
        }
    }
}
