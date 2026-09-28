
using System;
using UnityEngine;

[Serializable]
public class PhysicsStateCondition : Condition
{
    [SerializeField] private PhysicsState physicsState;

    private PhysicsModule physicsModule;

    public override void Initialize(ModuleFrame frame) { physicsModule = frame.GetModule<PhysicsModule>(); }

    public override bool IsSatisfied
    {
        get { return physicsModule.State == physicsState; }

        protected set { IsSatisfied = value; }
    }
}
