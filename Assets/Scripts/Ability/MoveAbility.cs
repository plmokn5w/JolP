
using System;
using UnityEngine;

[Serializable]
public class MoveAbility : Ability
{
    private ControlModule controlModule;
    private PhysicsModule physicsModule;
    private StatModule statModule;

    public override void Init(GameObject gameObject)
    {
        base.Init(gameObject);
        controlModule = gameObject.GetComponent<ControlModule>();
        physicsModule = gameObject.GetComponent<PhysicsModule>();
        statModule = gameObject.GetComponent<StatModule>();
    }

    public override bool CanActivate()
    {
        return /*base.CanActivate() && */true;
    }

    public override void Execute()
    {
        physicsModule.Move(controlModule.MoveDirection, statModule[StatType.MoveSpeed]);
    }
}
