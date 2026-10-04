
using System.Collections.Generic;
using UnityEngine;

public class GaugeModule : MonoBehaviour
{
    [SerializeField] private Dictionary<GaugeType, Gauge> gauges;

    public Gauge this[GaugeType type]
    {
        get
        {
            gauges.TryGetValue(type, out Gauge gauge);
            return gauge;
        }
    }
}
