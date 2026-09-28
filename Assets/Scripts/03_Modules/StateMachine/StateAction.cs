
using System;

[Serializable]
public abstract class StateAction
{
    public abstract void Initialize(ModuleFrame frame);

    public abstract void OnEnter();

    public abstract void OnUpdate();

    public abstract void OnExit();

    public virtual StateAction Clone()
    {
        StateAction clone = MemberwiseClone() as StateAction;
        return clone;
    }
}
