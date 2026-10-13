
using System;
using UnityEngine;
using System.Collections.Generic;
using R3;

[Serializable]
public class JumpAbility : Ability
{
    private HashSet<StateKey> allowedStates = new();

    [SerializeField] private List<StateKey> allowedStateList;

    private StateMachine stateMachine;
    private ControlModule controlModule;
    private PhysicsModule physicsModule;
    private StatModule statModule;
    private GaugeModule gaugeModule;

    public override void Init(GameObject gameObject)
    {
        base.Init(gameObject);
        stateMachine = gameObject.GetComponent<StateMachine>();
        controlModule = gameObject.GetComponent<ControlModule>();
        physicsModule = gameObject.GetComponent<PhysicsModule>();
        statModule = gameObject.GetComponent<StatModule>();
        gaugeModule = gameObject.GetComponent<GaugeModule>();
        foreach (var state in allowedStateList)
            allowedStates.Add(state);
        physicsModule.State.Where(state => state == PhysicsState.Ground).Subscribe(state => OnLanding());
        physicsModule.State.Where(state => state == PhysicsState.Aerial).Subscribe(state => OnAirborne());
    }

    public override bool CanActivate()
    {
        StateKey state = stateMachine.CurrentState;
        return allowedStates.Contains(state) && controlModule.Flags.HasFlag(CommandFlags.Jump) && gaugeModule[GaugeType.JumpCount] >= 1f;
    }

    public override void Execute()
    {
        physicsModule.Launch(statModule[StatType.JumpPower]);
        gaugeModule[GaugeType.JumpCount].Lose(1f);
    }

    private void OnLanding() { gaugeModule[GaugeType.JumpCount].Recover(gaugeModule[GaugeType.JumpCount].MaxValue); }

    private void OnAirborne()
    {
        if (!allowedStates.Contains(stateMachine.CurrentState))
            gaugeModule[GaugeType.JumpCount].Lose(1f);
    }
}
