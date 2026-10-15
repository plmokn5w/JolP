
using System;
using UnityEngine;
using System.Collections.Generic;
using R3;

[Serializable]
public class EnviromentStateTransition : StateTransition
{
    private HashSet<StateKey> landingStates = new();
    private HashSet<StateKey> airborneStates = new();
    [SerializeField] private StateKey onGroundState;
    [SerializeField] private List<StateKey> landingStateList;
    [SerializeField] private StateKey onAerialState;
    [SerializeField] private List<StateKey> airborneStateList;

    private StateMachine stateMachine;
    private PhysicsModule physicsModule;

    public override void Init(GameObject gameObject)
    {
        base.Init(gameObject);
        stateMachine = gameObject.GetComponent<StateMachine>();
        physicsModule = gameObject.GetComponent<PhysicsModule>();
        foreach (var state in landingStateList)
            landingStates.Add(state);
        foreach (var state in airborneStateList)
            airborneStates.Add(state);
        physicsModule.State.Where(state => state == PhysicsState.Ground).Subscribe(state => OnLanding());
        physicsModule.State.Where(state => state == PhysicsState.Aerial).Subscribe(state => OnAirborne());
    }

    public override bool Evaluate(out StateKey targetState)
    {
        targetState = default;
        return false;
    }

    private void OnLanding()
    {
        if (landingStates.Contains(stateMachine.CurrentState))
            stateMachine.TryChangeState(onGroundState);
    }

    private void OnAirborne()
    {
        if (airborneStates.Contains(stateMachine.CurrentState))
            stateMachine.TryChangeState(onAerialState);
    }
}
