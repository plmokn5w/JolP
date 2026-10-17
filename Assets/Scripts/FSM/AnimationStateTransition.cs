
using System;
using UnityEngine;

[Serializable]
public class AnimationStateTransition : StateTransition
{

    private RenderModule renderModule;
    private StateMachine stateMachine;

    public override void Init(GameObject gameObject)
    {
        base.Init(gameObject);
        renderModule = gameObject.GetComponent<RenderModule>();
        stateMachine = gameObject.GetComponent<StateMachine>();
    }

    public override bool Evaluate(out StateKey targetState)
    {
        if (renderModule.IsAnimationFinished() && stateMachine.TryGetCurrentData(out AnimationStateData data))
        {
            if (data.nextState is not null)
            {
                targetState = data.nextState;
                return true;
            }
        }
        targetState = default;
        return false;
    }
}
