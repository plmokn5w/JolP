
using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class StateTransition
{
    [field: SerializeField] public StateKey NextState { get; private set; }
    [SerializeReference] private List<Condition> conditions;

    public virtual void Initialize(ModuleFrame frame)
    {
        for (int i = 0; i < conditions.Count; i++)
            conditions[i].Initialize(frame);
    }

    public virtual bool IsSatisfied()
    {
        for (int i = 0; i < conditions.Count; i++)
        {
            if (!conditions[i].IsSatisfied)
                return false;
        }
        return true;
    }

    public StateTransition Clone()
    {
        StateTransition clone = MemberwiseClone() as StateTransition;
        clone.conditions = new List<Condition>();
        for (int i = 0; i < conditions.Count; i++)
            clone.conditions.Add(conditions[i].Clone());
        return clone;
    }
}
