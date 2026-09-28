
using System;
using UnityEngine;

[Serializable]
public class StateTimeExpiredCondition : Condition
{
    [SerializeField] private float time;

    private StateMachine stateMachine;

    public override void Initialize(ModuleFrame frame) { stateMachine = frame.GetModule<StateMachine>(); }

    public override bool IsSatisfied
    {
        get { return stateMachine.Timer >= time; }

        protected set { IsSatisfied = value; }
    }
}
