
using System;
using UnityEngine;

[Serializable]
public class MoveAbility : Ability
{
    private PhysicsModule physicsModule;
    private StatModule statModule;

    public override void Init(GameObject gameObject)
    {
        base.Init(gameObject);
        physicsModule = gameObject.GetComponent<PhysicsModule>();
        statModule = gameObject.GetComponent<StatModule>();
    }

    public override bool CanActivate()
    {
        return true;
    }

    public override void Execute()
    {
        Vector2 moveInput = WorldConfig.Instance.Input.Player.Move.ReadValue<Vector2>();
        physicsModule.Move(moveInput, statModule[StatType.MoveSpeed]);
    }
}
