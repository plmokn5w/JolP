
using UnityEngine;
using R3;
using System.Collections.Generic;
using System;

public class StateMachine : MonoBehaviour
{
    public StateKey CurrentState { get; private set; }
    [field: SerializeField] public float StateTimer { get; private set; }
    public Observable<StateKey> OnStateEnter => onStateEnter;
    public Observable<StateKey> OnStateExit => onStateExit;

    private Dictionary<StateKey, State> states = new();
    private Dictionary<Type, StateTransition> transitions = new();
    private readonly Subject<StateKey> onStateEnter = new();
    private readonly Subject<StateKey> onStateExit = new();

    [SerializeField] private List<StateSO> stateSOs;
    [SerializeReference] private List<StateTransition> transitionList;

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
        foreach (var stateSO in stateSOs)
        {
            State state = stateSO.CreateState();
            states.Add(stateSO.stateKey, state);
        }
        foreach (var transition in transitionList)
        {
            transition.Init(gameObject);
            transitions.Add(transition.GetType(), transition);
        }
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
