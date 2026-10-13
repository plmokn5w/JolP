
using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class CommandStateTransition : StateTransition
{
    private StateMachine stateMachine;
    private ControlModule controlModule;

    public override void Init(GameObject gameObject)
    {
        base.Init(gameObject);
        stateMachine = gameObject.GetComponent<StateMachine>();
        controlModule = gameObject.GetComponent<ControlModule>();
    }

    public override bool Evaluate(out StateKey targetState)
    {
        if (stateMachine.TryGetCurrentData(out CommandStateData data))
        {
            CommandFlags commandFlags = controlModule.Flags;
            List<CommandStateData.Pair> pairs = data.pairs;
            foreach (var pair in pairs)
            {
                if (commandFlags.HasFlag(pair.command))
                {
                    targetState = pair.state;
                    return true;
                }
            }
            if (data.noInputState is not null)
            {
                targetState = data.noInputState;
                return true;
            }
        }
        targetState = default;
        return false;
    }
}
