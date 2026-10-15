
using System;
using UnityEngine;
using R3;

[Serializable]
public class HitStateTransition : StateTransition
{
    private StateMachine stateMachine;
    private CombatModule combatModule;

    public override void Init(GameObject gameObject)
    {
        base.Init(gameObject);
        stateMachine = gameObject.GetComponent<StateMachine>();
        combatModule = gameObject.GetComponent<CombatModule>();
        combatModule.State.Subscribe(state => OnHit(state));
    }

    public override bool Evaluate(out StateKey targetState)
    {
        targetState = default;
        return false;
    }

    private void OnHit(CombatState combatState)
    {
        stateMachine.TryChangeState(WorldConfig.Instance.CombatStateMap[combatState]);
    }
}
