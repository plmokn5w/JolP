
using R3;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : Module
{
    public State State { get; private set; }
    public float Timer { get; private set; }

    public Subject<StateKey> StateChangeChannel { get; private set; }

    private Dictionary<StateKey, State> states;
    private List<StateAction> actions;
    private List<StateTransition> transitions;

    public void Initialize(StateMachineData data)
    {
        StateChangeChannel = new Subject<StateKey>();
        states = new Dictionary<StateKey, State>();
        for (int i = 0; i < data.states.Count; i++)
            states.Add(data.states[i].key, data.states[i].data.CreateState());
        actions = new List<StateAction>();
        for (int i = 0; i < data.actions.Count; i++)
            actions.Add(data.actions[i].Clone());
        transitions = new List<StateTransition>();
        for (int i = 0; i < data.transitions.Count; i++)
            transitions.Add(data.transitions[i].Clone());
        State = states[data.startState];
    }

    public override void Initialize(ModuleFrame frame)
    {
        base.Initialize(frame);
        foreach (var state in states.Values)
            state.Initialize(frame);
        for (int i = 0; i < actions.Count; i++)
            actions[i].Initialize(frame);
        for (int i = 0; i < transitions.Count; i++)
            transitions[i].Initialize(frame);
    }

    public void OnUpdate()
    {
        Timer += Time.deltaTime;
        for (int i = 0; i < actions.Count; i++)
            actions[i].OnUpdate();
        State.OnUpdate();
        for (int i = 0; i< transitions.Count; i++)
        {
            if (transitions[i].IsSatisfied())
            {
                TryChangeState(transitions[i].NextState);
                break;
            }
        }
    }

    public void TryChangeState(StateKey key)
    {
        if (states.ContainsKey(key) && State.CanTransitionTo(key))
            ChangeState(key);
    }

    private void ChangeState(StateKey key)
    {
        State nextState = states[key];
        for (int i = 0; i < actions.Count; i++)
            actions[i].OnExit();
        State.OnExit();
        State = nextState;
        Timer = 0f;
        StateChangeChannel.OnNext(key);
        for (int i = 0; i < actions.Count; i++)
            actions[i].OnEnter();
        State.OnEnter();
    }
}
