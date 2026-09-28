
using System;
using UnityEngine;

[Serializable]
public class SkillExecuteStateAction : StateAction
{
    [SerializeField] private SkillKey key;
    private SkillModule skillModule;

    public override void Initialize(ModuleFrame frame)
    {
        skillModule = frame.GetModule<SkillModule>();
        Debug.Log(skillModule);
    }

    public override void OnEnter()
    {
        Debug.Log(key);
        skillModule.ExecuteSkill(key);
    }

    public override void OnUpdate()
    {
    }

    public override void OnExit()
    {
    }
}
