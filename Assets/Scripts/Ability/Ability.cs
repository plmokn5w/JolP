
using System;
using UnityEngine;

[Serializable]
public abstract class Ability
{
    protected GameObject frame;

    public virtual void Init(GameObject gameObject)
    {
        frame = gameObject;
    }

    public abstract bool CanActivate();

    public abstract void Execute();
}
