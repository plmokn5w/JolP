
using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class CommandStateTransitionMap
{
    [SerializeField] private Dictionary<CommandFlags, StateKey> map = new Dictionary<CommandFlags, StateKey>();
    [field: SerializeField] public StateKey NextState { get; private set; }

    public StateKey this[CommandFlags flag]
    {
        get
        {
            if (map.TryGetValue(flag, out StateKey state))
                return state;
            return null;
        }
    }
}
