
using System;
using UnityEngine;

[Serializable]
public class NoInputCondition : Condition
{
    [SerializeField] private Command command;

    private ControlModule controlModule;

    public override void Initialize(ModuleFrame frame) { controlModule = frame.GetModule<ControlModule>(); }

    public override bool IsSatisfied
    {
        get { return !controlModule.Command.HasFlag(command); }

        protected set { IsSatisfied = value; }
    }
}
