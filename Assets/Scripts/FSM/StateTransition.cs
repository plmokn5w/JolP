
using System;

[Serializable]
public abstract class StateTransition
{
    public abstract bool Evaluate(out StateKey targetState);
}
