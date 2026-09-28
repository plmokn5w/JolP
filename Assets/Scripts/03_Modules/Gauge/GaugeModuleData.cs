
using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GaugeModuleData", menuName = "Scriptable Objects/Modules/GaugeModuleData")]
public class GaugeModuleData : ModuleData
{
    [Serializable]
    public class Pair
    {
        public GaugeType type;
        public float value;
    }

    [SerializeReference] public List<Pair> gauges;

    public override Module CreateModule()
    {
        GaugeModule module = new GaugeModule();
        module.Initialize(this);
        return module;
    }
}
