
using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SkillModuleData", menuName = "Scriptable Objects/Modules/SkillModuleData")]
public class SkillModuleData : ModuleData
{
    [Serializable]
    public class Pair
    {
        public SkillKey key;
        public SkillData data;
    }

    [SerializeReference] public List<Pair> skills;

    public override Module CreateModule()
    {
        SkillModule module = new SkillModule();
        module.Initialize(this);
        return module;
    }
}
