
using System;
using UnityEngine;

[Serializable]
public class InputCondition : Condition
{
    [SerializeField] private Command command;

    private ControlModule controlModule;

    public override void Initialize(ModuleFrame frame)
    {
        controlModule = frame.GetModule<ControlModule>();
        Debug.Log(controlModule);
    }

    public override bool IsSatisfied {
        get { return controlModule.Command.HasFlag(command); }

        protected set { IsSatisfied = value; }
    }
}
