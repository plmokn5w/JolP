
using UnityEngine;

public abstract class ControlModule : Module
{
    public FacingDirection FacingDirection { get; protected set; }
    public Vector2 MoveDirection { get; protected set; }

    public Command Command { get; protected set; }

    public override void Initialize(ModuleFrame frame)
    {
        base.Initialize(frame);
    }

    public abstract void OnUpdate();

    public void Lookat(FacingDirection direction) { FacingDirection = direction; }

    public void SetCommand(Command command) { Command |= command; }
}
