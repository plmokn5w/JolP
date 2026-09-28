
using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "RenderModuleData", menuName = "Scriptable Objects/Modules/RenderModuleData")]
public class RenderModuleData : ModuleData
{
    [Serializable]
    public class Pair
    {
        public StateKey key;
        public string name;
    }

    [SerializeReference] public List<Pair> states;

    public override Module CreateModule()
    {
        RenderModule module = new RenderModule();
        module.Initialize(this);
        return module;
    }
}
