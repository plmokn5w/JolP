
using System;
using System.Collections.Generic;

[Serializable]
public abstract class SkillAction
{
    public abstract void Action(List<ModuleFrame> targets);

    public abstract void Initialize(ModuleFrame frame);

    public virtual SkillAction Clone()
    {
        SkillAction clone = MemberwiseClone() as SkillAction;
        return clone;
    }
}
