
using System;

[Serializable]
public abstract class Condition
{
    public abstract bool IsSatisfied { get; protected set; }

    public abstract void Initialize(ModuleFrame frame);

    public virtual Condition Clone()
    {
        Condition clone = MemberwiseClone() as Condition;
        return clone;
    }
}
