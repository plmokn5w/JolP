
using System;

[Serializable]
public class MoveAbility : Ability
{
    private PhysicsModule physicsModule;
    private ControlModule controlModule;
    private StatModule statModule;

    public override void Initialize(ModuleFrame frame)
    {
        physicsModule = frame.GetModule<PhysicsModule>();
        controlModule = frame.GetModule<ControlModule>();
        statModule = frame.GetModule<StatModule>();
    }

    public override bool CanActivate()
    {
        return true;
    }

    public override void Execute()
    {
        physicsModule.Move(controlModule.MoveDirection, statModule[StatType.MoveSpeed]);
    }
}
