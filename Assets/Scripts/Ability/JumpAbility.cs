
using System;
using UnityEngine;
using R3;

[Serializable]
public class JumpAbility : Ability
{
    private ControlModule controlModule;
    private PhysicsModule physicsModule;
    private StatModule statModule;
    private GaugeModule gaugeModule;

    public override void Init(GameObject gameObject)
    {
        base.Init(gameObject);
        controlModule = gameObject.GetComponent<ControlModule>();
        physicsModule = gameObject.GetComponent<PhysicsModule>();
        statModule = gameObject.GetComponent<StatModule>();
        gaugeModule = gameObject.GetComponent<GaugeModule>();
        physicsModule.State.Where(physicsState => physicsState == PhysicsState.Ground).Subscribe(physicsState => OnLanding(physicsState));
    }

    public override bool CanActivate()
    {
        return controlModule.Flags.HasFlag(CommandFlags.Jump) && gaugeModule[GaugeType.JumpCount] >= 1f;
    }

    public override void Execute()
    {
        physicsModule.Launch(statModule[StatType.JumpPower]);
        gaugeModule[GaugeType.JumpCount].Lose(1f);
    }

    public void OnLanding(PhysicsState physicsState)
    {
        gaugeModule[GaugeType.JumpCount].Recover(gaugeModule[GaugeType.JumpCount].MaxValue);
    }
}
