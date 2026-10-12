
using System;
using UnityEngine;

[Serializable]
public abstract class StateTransition
{
    protected GameObject frame;

    public virtual void Init(GameObject gameObject) { frame = gameObject; }

    public abstract bool Evaluate(out StateKey targetState);
}
