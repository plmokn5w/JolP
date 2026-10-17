
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AttackAbility : Ability
{
    private HashSet<StateKey> allowedStates = new();

    [SerializeField] private List<StateKey> allowedStateList;

    private Transform transform;
    private StateMachine stateMachine;
    private ControlModule controlModule;

    public override void Init(GameObject gameObject)
    {
        base.Init(gameObject);
        transform = gameObject.transform;
        stateMachine = gameObject.GetComponent<StateMachine>();
        controlModule = gameObject.GetComponent<ControlModule>();
        foreach (var state in allowedStateList)
            allowedStates.Add(state);
    }

    public override bool CanActivate()
    {
        StateKey state = stateMachine.CurrentState;
        return allowedStates.Contains(state);
    }

    public override void Execute()
    {
        if (!stateMachine.TryGetCurrentData(out AttackStateData data))
            return;
        if (data.targetState is not null)
        {
            if (controlModule.Flags.HasFlag(CommandFlags.Attack))
                stateMachine.TryChangeState(data.targetState);
            return;
        }
        Vector3 orientedOffset = data.attackBoxOffset;
        orientedOffset.x *= (float)controlModule.LookDirection.Value;

        Vector3 attackCenter = transform.position + orientedOffset;
        Vector3 halfExtents = data.attackBoxSize * 0.5f;

        Collider[] hitColliders = Physics.OverlapBox(attackCenter, halfExtents, Quaternion.identity, data.targetLayer);

        foreach (var col in hitColliders)
        {
            if (col.gameObject == frame)
                continue;

            if (col.TryGetComponent<Hittable>(out var hittable))
            {
                HitData hitData = data.originHitData;
                Vector3 dir = hitData.knockbackDirection;
                dir.x *= (float)controlModule.LookDirection.Value;
                hitData.knockbackDirection = dir;

                hittable.Hit(hitData);
            }
        }
    }
}
