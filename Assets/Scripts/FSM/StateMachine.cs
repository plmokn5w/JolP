
using UnityEngine;
using System.Collections.Generic;

public class StateMachine : MonoBehaviour
{
    [field: SerializeField] public StateKey CurState { get; private set; }
    [SerializeField] private Dictionary<StateKey, State> states;

    public void TryChangeState(StateKey stateKey)
    {
        Debug.Log("아직 구현안함");
    }
}
