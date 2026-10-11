
using System;

[Serializable]
public class CommandStateTransition : StateTransition
{
    public override bool Evaluate(out StateKey targetState)
    {
        targetState = default;
        return false;
    }
}
