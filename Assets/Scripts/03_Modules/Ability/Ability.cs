
using System;

[Serializable]
public abstract class Ability
{
    public abstract void Initialize(ModuleFrame frame);

    public abstract bool CanActivate();

    public abstract void Execute();
}
