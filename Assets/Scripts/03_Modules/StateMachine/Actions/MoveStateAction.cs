
using System;
using UnityEngine;

[Serializable]
public class MoveStateAction : StateAction
{
    private ControlModule controlModule;
    private PhysicsModule physicsModule;
    private StatModule statModule;

    [SerializeField] private float speedScale;

    public override void Initialize(ModuleFrame frame)
    {
        controlModule = frame.GetModule<ControlModule>();
        physicsModule = frame.GetModule<PhysicsModule>();
        statModule = frame.GetModule<StatModule>();
    }

    public override void OnEnter()
    {
    }

    public override void OnUpdate()
    {
        physicsModule.Move(controlModule.MoveDirection, statModule[StatType.MoveSpeed] * speedScale);
    }

    public override void OnExit()
    {
        physicsModule.Move(Vector2.zero, 0f);
    }
}
