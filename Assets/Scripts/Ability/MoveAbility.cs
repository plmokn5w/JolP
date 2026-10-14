
using System;
using UnityEngine;
using System.Collections.Generic;
using R3;

[Serializable]
public class MoveAbility : Ability
{
    private HashSet<StateKey> allowedStates = new();

    [SerializeField] private List<StateKey> allowedStateList;

    private StateMachine stateMachine;
    private ControlModule controlModule;
    private PhysicsModule physicsModule;
    private StatModule statModule;

    public override void Init(GameObject gameObject)
    {
        base.Init(gameObject);
        stateMachine = gameObject.GetComponent<StateMachine>();
        controlModule = gameObject.GetComponent<ControlModule>();
        physicsModule = gameObject.GetComponent<PhysicsModule>();
        statModule = gameObject.GetComponent<StatModule>();
        foreach (var state in allowedStateList)
            allowedStates.Add(state);
        stateMachine.OnStateExit.Where(state => allowedStates.Contains(state)).Subscribe(state => Stop());
    }

    public override bool CanActivate()
    {
        return allowedStates.Contains(stateMachine.CurrentState);
    }

    public override void Execute()
    {
        if (!stateMachine.TryGetCurrentData(out MoveStateData data))
        {
            Debug.Log("데이터가 없음");
            return;
        }
        if (controlModule.Flags.HasFlag(CommandFlags.Move))
        {
            if (data.moveState is not null)
                stateMachine.TryChangeState(data.moveState);
            physicsModule.Move(controlModule.MoveDirection, statModule[StatType.MoveSpeed] * data.moveScale);
        }
        else
        {
            if (data.stopState is not null)
                stateMachine.TryChangeState(data.stopState);
            physicsModule.Move(controlModule.MoveDirection, statModule[StatType.MoveSpeed] * data.moveScale);
        }
    }

    public void Stop()
    {
        physicsModule.Move(Vector2.zero, 0f);
    }
}
