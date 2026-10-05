
using System;
using UnityEngine;

[Serializable]
public class JumpAbility : Ability
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
        return controlModule.Flags.HasFlag(CommandFlags.Jump) && physicsModule.State == PhysicsState.Ground;
    }

    public override void Execute()
    {
        physicsModule.Launch(statModule[StatType.JumpPower]);
    }
}
