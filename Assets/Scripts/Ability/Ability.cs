
using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public abstract class Ability
{
    protected GameObject frame;

    [SerializeField] private List<StateKey> allowedStateList;
    private HashSet<StateKey> allowedStates;

    public virtual void Init(GameObject gameObject) { frame = gameObject; }

    public abstract bool CanActivate();

    public abstract void Execute();
}
