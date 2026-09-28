
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "StateData", menuName = "Scriptable Objects/Data/StateData")]
public class StateData : ScriptableObject
{
    [SerializeReference] public List<StateAction> actions;
    [SerializeReference] public List<StateTransition> transitions;
    [SerializeReference] public List<StateKey> forbiddenKeys;

    public State CreateState()
    {
        State state = new State();
        state.Initialize(this);
        return state;
    }
}
