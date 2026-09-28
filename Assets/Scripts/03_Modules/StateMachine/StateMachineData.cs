
using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "StateMachineData", menuName = "Scriptable Objects/Modules/StateMachineData")]
public class StateMachineData : ModuleData
{
    [Serializable]
    public class Pair
    {
        public StateKey key;
        public StateData data;
    }

    [SerializeReference] public List<Pair> states;
    [SerializeReference] public List<StateAction> actions;
    [SerializeReference] public List<StateTransition> transitions;
    public StateKey startState;

    public override Module CreateModule()
    {
        StateMachine module = new StateMachine();
        module.Initialize(this);
        return module;
    }
}
