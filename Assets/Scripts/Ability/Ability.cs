
using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public abstract class Ability
{
    protected GameObject frame;
    private StateMachine stateMachine;

    [SerializeField] private List<StateKey> allowedStateList;
    private HashSet<StateKey> allowedStates;

    public virtual void Init(GameObject gameObject)
    {
        frame = gameObject;
        stateMachine = frame.GetComponent<StateMachine>();
        allowedStates = new HashSet<StateKey>();
        foreach (var state in allowedStateList)
            allowedStates.Add(state);
    }

    public abstract bool CanActivate();
    /*{
        return allowedStates.Contains(stateMachine.CurState);
    }*/

    public abstract void Execute();
}
