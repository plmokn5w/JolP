
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "StateSO", menuName = "Scriptable Objects/StateSO")]
public class StateSO : ScriptableObject
{
    public StateKey stateKey;
    [SerializeReference] public List<StateData> dataList;

    public State CreateState()
    {
        State state = new State();
        state.Init(dataList);
        return state;
    }
}
