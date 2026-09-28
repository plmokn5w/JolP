
using System;

[Serializable]
public class FallCondition : Condition
{
    private PhysicsModule physicsModule;

    public override void Initialize(ModuleFrame frame) { physicsModule = frame.GetModule<PhysicsModule>(); }

    public override bool IsSatisfied
    {
        get { return physicsModule.Velocity.y < 0f; }

        protected set { IsSatisfied = value; }
    }
}
