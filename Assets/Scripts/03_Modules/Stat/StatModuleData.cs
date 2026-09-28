
using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "StatModuleData", menuName = "Scriptable Objects/Modules/StatModuleData")]
public class StatModuleData : ModuleData
{
    [Serializable]
    public class Pair
    {
        public StatType type;
        public float value;
    }

    [SerializeReference] public List<Pair> stats;

    public override Module CreateModule()
    {
        StatModule module = new StatModule();
        module.Initialize(this);
        return module;
    }
}
