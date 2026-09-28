
using System.Collections.Generic;

public class GaugeModule : Module
{
    private Dictionary<GaugeType, Gauge> gauges;

    public void Initialize(GaugeModuleData data)
    {
        gauges = new Dictionary<GaugeType, Gauge>();
        for (int i = 0; i < data.gauges.Count; i++)
            gauges.Add(data.gauges[i].type, new Gauge(data.gauges[i].value));
    }

    public Gauge this[GaugeType type]
    {
        get
        {
            gauges.TryGetValue(type, out Gauge gauge);
            return gauge;
        }
    }
}
