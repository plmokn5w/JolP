
using System;
using UnityEngine;

[Serializable]
public class LookStateAction : StateAction
{
    private ControlModule controlModule;
    private RenderModule renderModule;

    public override void Initialize(ModuleFrame frame)
    {
        controlModule = frame.GetModule<ControlModule>();
        renderModule = frame.GetModule<RenderModule>();
    }

    public override void OnEnter()
    {
        if (controlModule.MoveDirection.x < 0f)
            controlModule.Lookat(FacingDirection.Left);
        if (controlModule.MoveDirection.x > 0f)
            controlModule.Lookat(FacingDirection.Right);
        renderModule.Lookat(controlModule.FacingDirection);
    }

    public override void OnUpdate()
    {
        if (controlModule.MoveDirection.x < 0f)
            controlModule.Lookat(FacingDirection.Left);
        if (controlModule.MoveDirection.x > 0f)
            controlModule.Lookat(FacingDirection.Right);
        renderModule.Lookat(controlModule.FacingDirection);
    }

    public override void OnExit()
    {
    }
}
