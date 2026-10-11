
using UnityEngine;
using R3;
using System.Collections.Generic;
using System;

public class StateMachine : MonoBehaviour
{
    public StateKey CurrentState { get; private set; }
    [field: SerializeField] public float StateTimer { get; private set; }

    public Observable<StateKey> OnStateEnter => onStateEnter;
    private readonly Subject<StateKey> onStateEnter = new();

    public Observable<StateKey> OnStateExit => onStateExit;
    private readonly Subject<StateKey> onStateExit = new();

    [SerializeField] private Dictionary<StateKey, State> states;
    [SerializeReference] private List<StateTransition> transitionList;
    private Dictionary<Type, StateTransition> transitions = new();

    [SerializeField] private StateKey startState;

    public bool TryGetState(StateKey key, out State state)
    {
        if (states.ContainsKey(key))
        {
            state = states[key];
            return true;
        }
        state = default;
        return false;
    }

    public bool TryGetCurrentData<T>(out T data) where T : StateData { return states[CurrentState].TryGetData(out data); }

    public bool TryChangeState(StateKey targetState)
    {
        if (targetState == CurrentState)
            return false;
        onStateExit.OnNext(CurrentState);
        CurrentState = targetState;
        StateTimer = 0f;
        onStateEnter.OnNext(CurrentState);
        return true;
    }

    private void Awake()
    {
        foreach (var state in states.Values)
            state.Init();
        foreach (var transition in transitions.Values)
            transitionList.Add(transition);
        CurrentState = startState;
    }

    private void Update()
    {
        StateTimer += Time.deltaTime;
        foreach (var transition in transitionList)
        {
            if (transition.Evaluate(out StateKey nextState))
            {
                TryChangeState(nextState);
                break;
            }
        }
    }
}
