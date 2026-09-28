
using System;

[Serializable]
public class JumpStateAction : StateAction
{
    private PhysicsModule physicsModule;
    private StatModule statModule;

    public override void Initialize(ModuleFrame frame)
    {
        physicsModule = frame.GetModule<PhysicsModule>();
        statModule = frame.GetModule<StatModule>();
    }

    public override void OnEnter()
    {
        physicsModule.Launch(statModule[StatType.JumpPower]);
    }

    public override void OnUpdate()
    {
    }

    public override void OnExit()
    {
    }
}
