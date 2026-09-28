
using UnityEngine;
using System.Collections.Generic;

public class State
{
    private StateMachine stateMachine;

    private List<StateAction> actions;
    private List<StateTransition> transitions;
    private HashSet<StateKey> forbiddenKeys;

    public void Initialize(StateData data)
    {
        actions = new List<StateAction>();
        for (int i = 0; i < data.actions.Count; i++)
            actions.Add(data.actions[i].Clone());

        transitions = new List<StateTransition>();
        for (int i = 0; i < data.transitions.Count; i++)
            transitions.Add(data.transitions[i].Clone());

        forbiddenKeys = new HashSet<StateKey>();
        for (int i = 0; i < data.forbiddenKeys.Count; i++)
            forbiddenKeys.Add(data.forbiddenKeys[i]);
    }

    public void Initialize(ModuleFrame frame)
    {
        if ((stateMachine = frame.GetModule<StateMachine>()) == null)
            Debug.Log("프레임에 상태머신 모듈이 없는데스웅;;");

        for (int i = 0; i < actions.Count; i++)
            actions[i].Initialize(frame);

        for (int i = 0; i< transitions.Count; i++)
            transitions[i].Initialize(frame);
    }

    public void OnEnter()
    {
        for (int i = 0; i < actions.Count; i++)
            actions[i].OnEnter();
    }

    public void OnUpdate()
    {
        for (int i = 0; i < actions.Count; i++)
            actions[i].OnUpdate();

        for (int i = 0; i < transitions.Count; i++)
        {
            if (stateMachine.State.Equals(this) && transitions[i].IsSatisfied())
            {
                stateMachine.TryChangeState(transitions[i].NextState);
                break;
            }
        }
    }

    public void OnExit()
    {
        for (int i = 0; i < actions.Count; i++)
            actions[i].OnExit();
    }

    public bool CanTransitionTo(StateKey key) { return !forbiddenKeys.Contains(key); }

    public State Clone()
    {
        State clone = MemberwiseClone() as State;

        clone.actions = new List<StateAction>();
        for (int i = 0; i < actions.Count; i++)
            clone.actions.Add(actions[i].Clone());

        clone.transitions = new List<StateTransition>();
        for (int i = 0; i < transitions.Count; i++)
            clone.transitions.Add(transitions[i].Clone());

        clone.forbiddenKeys = new HashSet<StateKey>();
        foreach (var key in forbiddenKeys)
            clone.forbiddenKeys.Add(key);
        return clone;
    }
}
